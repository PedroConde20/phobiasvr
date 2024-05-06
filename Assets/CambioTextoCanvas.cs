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


    public AudioSource[] audioSources;

    public bool sonidoTurbulencia = false;
    public bool sonidoAvisoTurbulencia = false;
    public bool sonidoObjetosTurbulencia = false;


    private bool sonidoTurbulenciaActivo = false;
    public Light luzParpadeante; // Referencia al componente Light que parpadeará
    public void Start()
    {
        if (luzParpadeante == null)
        {
            Debug.LogError("¡Error! No se ha asignado un componente de luz.");
        }
        // Obtener la referencia al script DoorController
        doorController = FindObjectOfType<DoorController>();
        if (doorController == null)
        {
            Debug.LogError("Error: DoorController no encontrado.");
        }
        StartCoroutine(EsperarYCambiarTexto());
    }

    void Update()
    {
        // Verificar si el texto actual contiene "turbulencia"
        if (textoCanvas.text.Contains("turbulencia"))
        {
            // Si el sonido de turbulencia no está activo, activarlo
            if (!sonidoTurbulenciaActivo)
            {
                sonidoTurbulenciaActivo = true;
                audioSources[2].Play();
            }
            // Iniciar el parpadeo de la luz
            StartCoroutine(ParpadearLuz(0.5f)); // Tiempo de espera de 0.5 segundos entre cambios de estado
        }
        else
        {
            // Si el sonido de turbulencia está activo, desactivarlo
            if (sonidoTurbulenciaActivo)
            {
                sonidoTurbulenciaActivo = false;
                audioSources[2].Stop();
            }
            // Detener el parpadeo de la luz
            StopCoroutine(ParpadearLuz(0.5f)); // Detener la corutina de parpadeo
            luzParpadeante.enabled = true; // Asegurarse de que la luz esté encendida
        }
    }
    // Método para controlar el parpadeo de la luz con una velocidad personalizada
    private IEnumerator ParpadearLuz(float tiempoEspera)
    {
        while (true)
        {
            luzParpadeante.enabled = !luzParpadeante.enabled; // Cambiar el estado de la luz (encendido/apagado)
            yield return new WaitForSeconds(tiempoEspera); // Esperar el tiempo especificado antes de cambiar nuevamente
        }
    }
    public IEnumerator EsperarYCambiarTexto()
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
            sonidoAvisoTurbulencia = true;
            if (sonidoAvisoTurbulencia)
            {
                // Reproducir el tercer sonido (índice 1) sin loop
                audioSources[1].Play();
                Debug.Log("El Sonido de aviso turbulencia esta en " + sonidoAvisoTurbulencia);
            }
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
        personSitDown.ahoratepuedesparar = true;
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
    public void ActualizarContador()
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

            Debug.Log("El Sonido de turbulencia esta en " + sonidoTurbulencia);
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
                if (personSitDown.sentado == false)
                {
                    personSitDown.enabled = false;
                    Debug.Log("Entro a la excepcion para detener los codigos de turbulencia");
                }
            }

            // Cambiar el texto del canvas
            textoCanvas.text = "¡Turbulencia superada! Deberias abrir la puerta e ir a tu asiento para aterrizar.";

            Debug.Log("El Sonido de turbulencia esta en " + sonidoTurbulencia);
            final = true;
            Debug.Log("la variable final esta en:" + "" + final);
        }
    }

    public void OnTriggerEnter(Collider other)
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