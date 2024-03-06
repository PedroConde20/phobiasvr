using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContadorAire : MonoBehaviour
{
    public float tiempoLimiteMinutos = 5f;
    private float tiempoRestante;

    public Text textoCanvas;

    void Start()
    {
        // Desactiva el contador al principio
        textoCanvas.gameObject.SetActive(false);

        // Inicia la secuencia de mensajes
        StartCoroutine(MostrarMensajes());
    }

    IEnumerator MostrarMensajes()
    {
        // Muestra el primer mensaje durante 10 segundos
        textoCanvas.text = "Debes escapar de esta cueva! Puedes encontrar algunos objetos que te ayudar�n, linterna y un Pico";
        textoCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);

        // Muestra el segundo mensaje durante 10 segundos
        textoCanvas.text = "Asegurate de llevar la linterna y pico, los necesitaras!";
        yield return new WaitForSeconds(10f);

        // Muestra el contador
        tiempoRestante = tiempoLimiteMinutos * 60f; // Convierte minutos a segundos
        textoCanvas.text = ""; // Limpia el texto
        textoCanvas.gameObject.SetActive(true);

        // Inicia la cuenta regresiva
        InvokeRepeating("ActualizarContador", 0f, 1f); // Llama a ActualizarContador cada segundo
    }

    void ActualizarContador()
    {
        // Verifica si a�n hay tiempo restante
        if (tiempoRestante > 0f)
        {
            tiempoRestante -= 1f;

            // Convierte el tiempo restante a minutos y segundos
            float minutos = Mathf.FloorToInt(tiempoRestante / 60f);
            float segundos = Mathf.FloorToInt(tiempoRestante % 60f);

            // Actualiza el texto del Canvas
            textoCanvas.text = $"Sal de la cueva antes de quedarte sin aire! Te quedan {minutos:00}:{segundos:00}";
        }
        else
        {
            // Si se agota el tiempo, realiza las acciones que necesites aqu�
            // Por ejemplo, puedes reiniciar el nivel o mostrar un mensaje de que se qued� sin aire.
            Debug.Log("�Te quedaste sin aire!");
            Application.Quit();
        }
    }
}
