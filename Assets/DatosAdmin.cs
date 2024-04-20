using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatosAdmin : MonoBehaviour
{
    public string url = "https://unityvrproject.000webhostapp.com/SelectMenu.php"; // Reemplaza esto con la URL de tu script PHP en el servidor



    public void ObtenerDatosAdminDesdeBoton()
    {
        StartCoroutine(ObtenerDatosAdmin());
    }
    public IEnumerator ObtenerDatosAdmin()
    {
        // Obtiene el nombre de usuario y el rol DESDE OTRO CODIGO Web.loggedInUsername
        string[] parts = Web.loggedInUsername.Split('.');
        string nombreUsuarioAdministrador = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;
        string role = (parts.Length > 0) ? parts[0] : "Unknown Role";

        Debug.Log(nombreUsuarioAdministrador);
        // Construir la URL completa con el nombre de usuario del administrador como parámetro
        string urlCompleta = url + "?nombreUsuarioAdministrador=" + nombreUsuarioAdministrador;

        Debug.Log(urlCompleta);

        using (UnityWebRequest www = UnityWebRequest.Get(urlCompleta))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener los datos del administrador: " + www.error);
            }
            else
            {
                // Obtener los datos recuperados del servidor
                string datosRecibidos = www.downloadHandler.text;

                // Dividir los datos por saltos de línea para separar cada conjunto de datos
                string[] conjuntosDeDatos = datosRecibidos.Split('\n');

                // Recorrer cada conjunto de datos y mostrarlos uno por uno
                foreach (string conjuntoDeDatos in conjuntosDeDatos)
                {
                    // Dividir el conjunto de datos en columnas
                    string[] columnas = conjuntoDeDatos.Split(',');

                    // Si hay suficientes columnas
                    if (columnas.Length >= 6)
                    {
                        // Obtener los datos del conjunto de datos
                        string nombreCompleto = columnas[0] + " " + columnas[1] + " " + columnas[2];
                        string fobia = columnas[3];
                        string nivel = columnas[4];
                        string duracion = columnas[5];

                        // Mostrar los datos en el debug
                        Debug.Log("nombreCompleto: " + nombreCompleto + " fobia: " + fobia + " nivel: " + nivel + " duracion: " + duracion);
                    }
                }
            }
        }
    }
}