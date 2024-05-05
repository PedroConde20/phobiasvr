using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonNivel1 : MonoBehaviour
{

    [Space]
    [Tooltip("Animator for button animation")]
    public Animator anim;

    // Referencia al objeto de pantalla negra
    public GameObject pantallaNegra;

    public float activo = 1.0f;
    // Referencia al Canvas que quieres activar
    public GameObject canvas;


    public GameObject Revista;
    public GameObject Caja;
    public GameObject Araña;
    public GameObject ObjetoFinal;

    public CameraControllerAirplane cameraController; // Referencia al script CameraController
    // Variable para verificar si el jugador está cerca

    public bool ActivarTextosNivel1 = false;
    public void Start()
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

    }
    void OnTriggerEnter(Collider other)
    {
        if (anim != null)
            anim.SetTrigger("ButtonPress");

        Debug.Log("Cambio de escena a la numero 1");

        // Activar el oscurecimiento de pantalla
        StartCoroutine(OscurecerYDespuesAclarar());

    }

    IEnumerator OscurecerYDespuesAclarar()
    {

        Debug.Log("Se esta iniciando el oscurecimiento de pantalla");
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


        // Esperar 3 segundos antes de aclarar la pantalla automáticamente
        yield return new WaitForSeconds(3f);


        ObjetoFinal.SetActive(false);
        Araña.SetActive(false);

        Caja.SetActive(false);
        Revista.SetActive(false);
        // Activar el Canvas
        canvas.SetActive(true);

        // Aclarar la pantalla
        float tiempoInicioAclarar = Time.time;
        float duracionAclarar = 1f;

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
        // Destruir objetos con los tags "Araña" y "ObjetosFinal"
        ActivarTextosNivel1 = true;

        Debug.Log("El ActivarTextoNivel1 esta en: " + ActivarTextosNivel1);
    }
}
