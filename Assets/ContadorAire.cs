using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorAire : MonoBehaviour
{
    public float tiempoLimiteMinutos = 5f;
    private float tiempoRestante;

    public Text textoCanvas;

    void Start()
    {
        tiempoRestante = tiempoLimiteMinutos * 60f; // Convierte minutos a segundos

        // Inicia la cuenta regresiva
        InvokeRepeating("ActualizarContador", 0f, 1f); // Llama a ActualizarContador cada segundo
    }

    void ActualizarContador()
    {
        // Verifica si aún hay tiempo restante
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
            // Si se agota el tiempo, realiza las acciones que necesites aquí
            // Por ejemplo, puedes reiniciar el nivel o mostrar un mensaje de que se quedó sin aire.
            Debug.Log("¡Te quedaste sin aire!");
        }
    }
}
