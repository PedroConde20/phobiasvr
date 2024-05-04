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
            textoCanvas.text = "Deberías ir al baño, en unos minutos el avión aterrizará. , Parate con la letra B";
        }
        if (personSitDown != null)
        {
            // Esperar hasta que la persona se siente y siguienteTexto sea true
            while (!personSitDown.siguientetexto)
            {
                yield return null;
            }
            doorController.CloseDoor();
            // Cambiar el texto del canvas
            textoCanvas.text = "...";
        }
        // Esperar hasta que pantallaNegraActiva sea true
        while (!personSitDown.pantallaNegraActiva)
        {
            yield return null;
        }

        // Esperar 3 segundos después de que pantallaNegraActiva sea true
        yield return new WaitForSeconds(3f);

        // Activar el booleano algoAndaMal
        algoAndaMal = true;

        // Desactivar el componente DoorController para evitar que funcione
        if (algoAndaMal == true)
        {
            doorController.enabled = false;
            doorController.CloseDoor();
        }
        // Cambiar el texto del canvas
        textoCanvas.text = "Oh... algo anda mal allá afuera, tal vez debas pararte con B e investigar.";
        personSitDown.ahoratepuedesparar=true;
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
        // Verifica si aún hay tiempo restante
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
                // Reiniciar el estado después de que termine la turbulencia
                algoAndaMal = false;

                if (personSitDown.sentado)
                {
                    textoCanvas.text = "¡Parate con B , la turbulencia se superó";
                }
                else if (personSitDown.sentado == false)
                {
                    personSitDown.enabled = false;
                    Debug.Log("Entro a la excepcion para detener los codigos de turbulencia");
                }
            }

            // Cambiar el texto del canvas
            textoCanvas.text = "¡Turbulencia superada! Deberias abrir la puerta e ir a tu asiento para aterrizar.";

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
                textoCanvas.text = "Detrás de las cortinas está el baño, abre su puerta con E";
            }
        }
        else if (other.CompareTag("IndicadorTexto2"))
        {
            Destroy(other.gameObject);

            // Cambiar el texto del canvas
            if (textoCanvas != null)
            {
                textoCanvas.text = "Enciende la luz con T, vuelve a cerrar la puerta y acércate al inodoro.";
            }
        }
    }
}