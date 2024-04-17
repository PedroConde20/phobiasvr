using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CronometroController : MonoBehaviour
{
    public Text NombreyCronometro;
    public float tiempoTranscurrido = 0f;
    public bool cronometroActivo = true;
    public int FobiaID;
    public int Nivel;
    public bool esInvitado = false;
    public float tiempotrans;

    void Start()
    {
        // Verifica si el nombre de usuario está disponible
        if (!string.IsNullOrEmpty(Web.loggedInUsername))
        {
            // Obtiene el nombre de usuario y el rol
            string[] parts = Web.loggedInUsername.Split('.');
            string username = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;

            // Actualiza el texto de bienvenida con el nombre de usuario y el rol

            Debug.Log(username + " " + FormatTime(tiempoTranscurrido)) ;
        }
        else
        {
            // Si el nombre de usuario no está disponible, muestra un mensaje genérico
            //NombreyCronometro.text = "Usuario sin sesión";
            esInvitado = true;
        }
    }
    void Update()
    {
        // Obtiene el nombre de usuario y el rol
        string[] parts = Web.loggedInUsername.Split('.');
        string username = (parts.Length > 1) ? parts[1] : Web.loggedInUsername;
        if (cronometroActivo)
        {
            tiempoTranscurrido += Time.deltaTime;
            NombreyCronometro.text = username + " " + FormatTime(tiempoTranscurrido);
        }
    }

    public string FormatTime(float time)
    {
        int minutos = Mathf.FloorToInt(time / 60);
        int segundos = Mathf.FloorToInt(time % 60);
        return minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    // Método para pausar o reanudar el cronómetro
    public void ToggleCronometro(bool active)
    {
        cronometroActivo = active;
    }
}
