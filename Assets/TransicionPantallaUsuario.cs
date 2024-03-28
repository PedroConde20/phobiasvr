using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransicionPantallaUsuario : MonoBehaviour
{
    public bool estaactualizado = true;
    public bool pantallaNegraCompleta = false;
    public GameObject pantallaNegra;
    public CameraController cameraController;
    public Material cieloOscuro; // Asigna el material de cielo oscuro desde el Inspector
    public Text textoCanvas;
    public GameObject objetoADeshabilitar; // El GameObject que quieres deshabilitar
    void Start()
    {
        // Crear un objeto negro que cubra toda la pantalla
        pantallaNegra = CrearPantalla(Color.black);
    }

    private GameObject CrearPantalla(Color color)
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
        return pantallaNegra;
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

            textoCanvas.text = "Fase 1 completada!";
            yield return null;
        }

        // Asegurarse de que la pantalla esté completamente oscura al finalizar
        pantallaNegra.GetComponent<UnityEngine.UI.Image>().color = Color.black;

        // Esperar 2 segundos antes de iniciar el aclarado
        yield return new WaitForSeconds(2f);



        pantallaNegraCompleta = true;
        Debug.Log("la pantalla esta completamente negra asi que se cambia el cielo");
        // Cambiar el cielo a oscuro cuando la pantalla esté completamente negra
        CambiarCielo(cieloOscuro);
        // Iniciar el aclarado de la pantalla después de esperar
        if (objetoADeshabilitar != null)
        {
            objetoADeshabilitar.SetActive(false);
        }

        StartCoroutine(AclararPantalla());
    }

    private IEnumerator AclararPantalla()
    {
        float tiempoInicioAclarar = Time.time;
        float duracionAclarar = 5f; // Aclarar en 5 segundos

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
        textoCanvas.text = "Pasaron varias horas desde el despegue, al parecer esta lloviendo";
    }

    // Método para cambiar el cielo
    private void CambiarCielo(Material nuevoCielo)
    {
        RenderSettings.skybox = nuevoCielo;
    }

    void Update()
    {
        // Verificar si la variable finishturbulence es true en el CameraController
        if (cameraController != null && cameraController.finishturbulence && estaactualizado)
        {

            // Iniciar la transición de pantalla
            StartCoroutine(OscurecerPantalla());
            estaactualizado = false;
        }
    }
}
