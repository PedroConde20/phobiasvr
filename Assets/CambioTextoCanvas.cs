using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambioTextoCanvas : MonoBehaviour
{
    public bool estaactualizado = true;
    public Text textoCanvas;
    public PersonSitDown personSitDown; // Referencia al script PersonSitDown
    public bool algoAndaMal = false;
    public CameraControllerAirplane cameraControllerAirplane;
    public TurbulenciaAvion turbulenciaAvion;
    // Referencia al script DoorController
    public DoorController doorController;
    public float tiempoLimiteMinutos = 1f;
    public float tiempoRestante;
    public bool final = false;
    private void Start()
    {
        // Obtener la referencia al script DoorController
        doorController = FindObjectOfType<DoorController>();
        if (doorController == null)
        {
            Debug.LogError("Error: DoorController no encontrado.");
        }
        StartCoroutine(EsperarYCambiarTexto());
    }

    private IEnumerator EsperarYCambiarTexto()
    {
        yield return new WaitForSeconds(10f);

        if (textoCanvas != null)
        {
            textoCanvas.text = "Deber�as ir al ba�o, en unos minutos el avi�n aterrizar�.";
        }
        if (personSitDown != null)
        {
            // Esperar hasta que la persona se siente y siguienteTexto sea true
            while (!personSitDown.siguientetexto)
            {
                yield return null;
            }

            // Cambiar el texto del canvas
            textoCanvas.text = "...";
        }
        // Esperar hasta que pantallaNegraActiva sea true
        while (!personSitDown.pantallaNegraActiva)
        {
            yield return null;
        }

        // Esperar 3 segundos despu�s de que pantallaNegraActiva sea true
        yield return new WaitForSeconds(3f);

        // Activar el booleano algoAndaMal
        algoAndaMal = true;

        // Desactivar el componente DoorController para evitar que funcione
        if (algoAndaMal == true)
        {
            doorController.enabled = false;
        }
        // Cambiar el texto del canvas
        textoCanvas.text = "Oh... algo anda mal all� afuera, tal vez debas pararte con F e investigar.";

        // Esperar hasta que textoCronometro sea true
        while (!personSitDown.textoCronometro)
        {
            yield return null;
        }

        // Inicializar el tiempo restante
        tiempoRestante = tiempoLimiteMinutos * 60;

        // Inicia la cuenta regresiva
        InvokeRepeating("ActualizarContador", 0f, 1f);

    }
    private void ActualizarContador()
    {
        // Verifica si a�n hay tiempo restante
        if (tiempoRestante > 0f)
        {
            tiempoRestante -= 1f;

            // Convierte el tiempo restante a minutos y segundos
            float minutos = Mathf.FloorToInt(tiempoRestante / 60f);
            float segundos = Mathf.FloorToInt(tiempoRestante % 60f);

            // Actualiza el texto del Canvas
            textoCanvas.text = $"Espera a que termine la turbulencia en {minutos:00}:{segundos:00}";
        }
        else
        {
            if (doorController != null && cameraControllerAirplane != null && turbulenciaAvion != null)
            {
                cameraControllerAirplane.enabled = false;
                turbulenciaAvion.enabled = false;

                doorController.enabled = true; // Vuelve a activar DoorController
                // Reiniciar el estado despu�s de que termine la turbulencia
                algoAndaMal = false;
                personSitDown.enabled = false;
                Debug.Log("Entro a la excepcion para detener los codigos de turbulencia");
            }

            // Cambiar el texto del canvas
            textoCanvas.text = "�Turbulencia superada! Deberias abrir la puerta e ir a tu asiento para aterrizar.";
            final = true;
            Debug.Log("la variable final esta en:" + "" + final);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IndicadorTexto"))
        {
            Destroy(other.gameObject);

            // Cambiar el texto del canvas
            if (textoCanvas != null)
            {
                textoCanvas.text = "Detr�s de las cortinas est� el ba�o, abre su puerta con E";
            }
        }
        else if (other.CompareTag("IndicadorTexto2"))
        {
            Destroy(other.gameObject);

            // Cambiar el texto del canvas
            if (textoCanvas != null)
            {
                textoCanvas.text = "Enciende la luz con T, vuelve a cerrar la puerta y ac�rcate al inodoro.";
            }
        }
    }
}