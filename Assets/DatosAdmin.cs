using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DatosAdmin : MonoBehaviour
{
    public string url = "https://unityvrproject.000webhostapp.com/SelectMenu.php";
    public Text textoPrefab;
    public Transform contenedorDeTexto;

    public ScrollRect scrollView;

    public void ObtenerDatosAdminDesdeBoton()
    {
        StartCoroutine(ObtenerDatosAdmin());
    }

    public IEnumerator ObtenerDatosAdmin()
    {
        string[] parts = Web.loggedInUsername.Split('.');
        string nombreUsuarioAdministrador = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;

        string urlCompleta = url + "?nombreUsuarioAdministrador=" + nombreUsuarioAdministrador;

        using (UnityWebRequest www = UnityWebRequest.Get(urlCompleta))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener los datos del administrador: " + www.error);
            }
            else
            {
                string datosRecibidos = www.downloadHandler.text;

                string[] conjuntosDeDatos = datosRecibidos.Split('\n');

                foreach (string conjuntoDeDatos in conjuntosDeDatos)
                {
                    string[] columnas = conjuntoDeDatos.Split(',');

                    if (columnas.Length >= 6)
                    {
                        string nombreCompleto = columnas[0] + " " + columnas[1] + " " + columnas[2];
                        string fobia = columnas[3];
                        string nivel = columnas[4];
                        string duracion = columnas[5];
                        string textoFormateado = string.Format("{0,-30} {1,-20} {2,-20} {3}", nombreCompleto, fobia, nivel, duracion);
                        Text nuevoTexto = Instantiate(textoPrefab, contenedorDeTexto);
                        nuevoTexto.text = textoFormateado;
                        nuevoTexto.transform.SetParent(contenedorDeTexto, false);
                        Debug.Log(nombreCompleto + " " +  fobia + " " +  nivel + " "  + duracion);
                    }
                }

                // Se ajusta el conttenido del ScrollView
                Canvas.ForceUpdateCanvases();
                scrollView.normalizedPosition = new Vector2(0, 0);
            }
        }
    }
}