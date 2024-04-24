using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransicionDeInicio : MonoBehaviour
{
    public GameObject pantallaNegra;
    public GameObject pantallaBlanca;
    public Canvas primerCanvas;
    public Canvas segundoCanvas;

    void Start()
    {
        // Crear un objeto negro que cubra toda la pantalla
        pantallaNegra = CrearPantalla(Color.black);

        // Crear un objeto blanco para la transición inversa
        pantallaBlanca = CrearPantalla(Color.white);
        pantallaBlanca.SetActive(false);

        // Iniciar la transición al inicio del programa
        StartCoroutine(AclararPantalla(pantallaNegra));
    }

    private GameObject CrearPantalla(Color color)
    {
        GameObject pantalla = new GameObject("Pantalla");
        pantalla.transform.parent = Camera.main.transform;
        pantalla.transform.localPosition = new Vector3(0, 0, Camera.main.nearClipPlane + 0.1f);
        pantalla.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        pantalla.AddComponent<CanvasRenderer>();
        pantalla.AddComponent<Image>().color = color;
        pantalla.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        pantalla.SetActive(false);
        return pantalla;
    }

    private IEnumerator AclararPantalla(GameObject pantalla)
    {
        float tiempoInicioAclarar = Time.time;
        float duracionAclarar = 5f; // Duración de la transición en segundos

        pantalla.SetActive(true);

        while (Time.time - tiempoInicioAclarar < duracionAclarar)
        {
            float proporcionAclarar = (Time.time - tiempoInicioAclarar) / duracionAclarar;

            // Ajustar la opacidad del objeto para aclarar la pantalla
            Color colorActual = pantalla.GetComponent<Image>().color;
            pantalla.GetComponent<Image>().color = new Color(colorActual.r, colorActual.g, colorActual.b, 1 - proporcionAclarar);

            yield return null;
        }

        // Desactivar la pantalla al finalizar el aclarado (excepto para la pantalla blanca)
        if (pantalla != pantallaBlanca)
        {
            pantalla.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto Player colisiona con un objeto que tenga el tag "Salida"
        if (other.CompareTag("Salida"))
        {
            // Activar el segundo canvas y desactivar el primer canvas
            segundoCanvas.transform.localPosition = new Vector3(0, 0, Camera.main.nearClipPlane + 0.01f);
            segundoCanvas.gameObject.SetActive(true);
            primerCanvas.gameObject.SetActive(false);

            // Activar la pantalla blanca sin transición
            pantallaBlanca.SetActive(true);
        }
    }
}
