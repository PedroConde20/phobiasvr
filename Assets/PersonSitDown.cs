using System.Collections;
using UnityEngine;

public class PersonSitDown : MonoBehaviour
{
    public bool estaactualizado = true;
    public float distanciaSilla = 1.5f;
    public bool playerCerca = false;
    public Transform playerTransform;
    public bool sentado = false;
    public CharacterController playerController;

    private bool primeraColision = true;
    public GameObject pantallaNegra;
    public bool pantallaNegraActiva = false;

    public TurbulenciaAvion turbulenciaAvion; // Referencia al script de turbulencia
    public bool activarTurbulencia = false;

    public int vecesSentado = 0;

    public CameraControllerAirplane cameraController; // Referencia al script CameraController

    public float magnitudeperfectas = 0.05f;

    public bool siguientetexto = false;
    private void Start()
    {
        // Crear un objeto negro que cubra toda la pantalla
        pantallaNegra = new GameObject("PantallaNegra");
        pantallaNegra.transform.parent = Camera.main.transform;
        pantallaNegra.transform.localPosition = new Vector3(0, 0, Camera.main.nearClipPlane + 0.1f);
        pantallaNegra.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        pantallaNegra.AddComponent<CanvasRenderer>();
        pantallaNegra.AddComponent<UnityEngine.UI.Image>().color = Color.black;
        pantallaNegra.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        pantallaNegra.SetActive(false);
        // Obtener la referencia al script CameraController
        cameraController = Camera.main.GetComponent<CameraControllerAirplane>();
        if (cameraController == null)
        {
            Debug.LogError("Error: CameraController script no encontrado en la cámara principal.");
        }
    }

    private void Update()
    {
        if (playerCerca)
        {
            if (!sentado)
            {
                if (primeraColision)
                {
                    Sentarse();
                    StartCoroutine(OscurecerPantalla());
                    primeraColision = false;
                    siguientetexto = true;
                    vecesSentado = 1;
                    Debug.Log(siguientetexto + "" + "siguiente texto esta en true");
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    vecesSentado++;
                    // A partir de la segunda vez, el jugador puede levantarse o sentarse al presionar la tecla F.
                    if (sentado)
                    {
                        Pararse();
                    }
                    else
                    {
                        Sentarse();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Al presionar F para levantarse, activar el cronómetro para la turbulencia
                    activarTurbulencia = true;
                    Pararse();
                }
            }
        }

        // Activar la turbulencia 5 segundos después de levantarse
        if (activarTurbulencia)
        {
            StartCoroutine(ActivarTurbulenciaDespuesDeEspera());
            activarTurbulencia = false;

        }
        if (cameraController != null)
        {
            Debug.Log("Cameracontroller no es nulo");
            // Verificar si tanto la turbulencia como estar sentado están activados
            if (sentado && vecesSentado > 1)
            {
                Debug.Log("Esta entrando al programa shake");
                // Activar el movimiento de la cámara como turbulencia
                cameraController.Shake(magnitudeperfectas);
            }
            else if (!sentado)
            {
                // Restablecer la posición de la cámara al levantarse
                cameraController.StopShaking();
            }

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = true;
            playerTransform = other.transform;
            playerController = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;
        }
    }

    private void Sentarse()
    {
        playerController.enabled = false;
        playerTransform.position = transform.position;
        playerTransform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        sentado = true;
    }

    private void Pararse()
    {
        playerController.enabled = true;
        playerTransform.position = new Vector3(playerTransform.position.x, 0.0f, playerTransform.position.z);
        sentado = false;

        if (pantallaNegraActiva)
        {
            // Desactivar la pantalla negra al levantarse
            StartCoroutine(AclararPantalla());
            pantallaNegraActiva = false;
        }
        // Restablecer la posición de la cámara al levantarse
        cameraController.StopShaking();
    }

    private IEnumerator OscurecerPantalla()
    {
        pantallaNegra.SetActive(true);

        float tiempoInicioOscurecimiento = Time.time;
        float duracionOscurecimiento = 5f;

        while (Time.time - tiempoInicioOscurecimiento < duracionOscurecimiento)
        {
            float proporcionOscurecimiento = (Time.time - tiempoInicioOscurecimiento) / duracionOscurecimiento;

            // Ajustar la opacidad del objeto negro
            Color colorActual = pantallaNegra.GetComponent<UnityEngine.UI.Image>().color;
            pantallaNegra.GetComponent<UnityEngine.UI.Image>().color = new Color(colorActual.r, colorActual.g, colorActual.b, proporcionOscurecimiento);

            yield return null;
        }

        // Asegurarse de que la pantalla esté completamente oscura al finalizar
        pantallaNegra.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        pantallaNegraActiva = true;

        // Esperar 3 segundos antes de aclarar la pantalla automáticamente
        yield return new WaitForSeconds(3f);

        StartCoroutine(AclararPantalla());
    }

    private IEnumerator AclararPantalla()
    {
        float tiempoInicioAclarar = Time.time;
        float duracionAclarar = 2f; // Puedes ajustar la duración según tus preferencias

        while (Time.time - tiempoInicioAclarar < duracionAclarar)
        {
            float proporcionAclarar = (Time.time - tiempoInicioAclarar) / duracionAclarar;

            // Ajustar la opacidad del objeto negro para aclarar la pantalla
            Color colorActual = pantallaNegra.GetComponent<UnityEngine.UI.Image>().color;
            pantallaNegra.GetComponent<UnityEngine.UI.Image>().color = new Color(colorActual.r, colorActual.g, colorActual.b, 1 - proporcionAclarar);

            yield return null;
        }

        // Desactivar la pantalla negra al finalizar el aclarado
        pantallaNegra.SetActive(false);
    }

    private IEnumerator ActivarTurbulenciaDespuesDeEspera()
    {
        // Esperar 5 segundos antes de activar la turbulencia
        yield return new WaitForSeconds(5f);

        // Activar la turbulencia después de 5 segundos
        turbulenciaAvion.turbulenciaActivada = true;
    }
}