using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class DatosAntesDeSalir : MonoBehaviour
{
    public CronometroController cronometroController;

    void Start()
    {
        // Encuentra el CronometroController en la escena
        cronometroController = FindObjectOfType<CronometroController>();

        if (cronometroController == null)
        {
            Debug.LogError("No se encontró el CronometroController en la escena.");
        }
    }

    public void OnApplicationQuit()
    {
        if (cronometroController != null)
        {
            if (cronometroController.esInvitado == true)
            {
                Debug.Log("Es invitado , no se rescata datos de el");
            }
            else
            {
                // Obtener el tiempo transcurrido del cronómetro
                float tiempoTranscurrido = cronometroController.tiempoTranscurrido;

                // Obtener el ID del nivel
                int nivelID = cronometroController.Nivel;

                // Obtener el nombre de usuario
                string[] parts = Web.loggedInUsername.Split('.');
                string username = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;

                
                // Llamar al método para enviar los datos al servidor
                StartCoroutine(EnviarDatosAlServidor(username, nivelID, cronometroController.FormatTime(tiempoTranscurrido)));
                Debug.Log("Esta en el else para enviar los datos");
            }
        }
    }

    IEnumerator EnviarDatosAlServidor(string username, int nivelID, string tiempoTranscurrido)
    {
        // Construir la URL del script PHP en el servidor
        string url = "https://unityvrproject.000webhostapp.com/guardarTiempo.php";

        // Crear un formulario para enviar los datos al script PHP
        WWWForm form = new WWWForm();
        form.AddField("nombreUsuario", username);
        form.AddField("nivelID", nivelID);
        form.AddField("tiempo", tiempoTranscurrido.ToString());

        Debug.Log("En enviar datos al servidor esta llegando: " + username + " " + nivelID + " "+ tiempoTranscurrido + " del onapplicationquit");
        // Enviar la solicitud al servidor
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al enviar los datos al servidor: " + www.error);
            }
            else
            {
                Debug.Log("Datos enviados al servidor correctamente");
            }
        }
    }
}
