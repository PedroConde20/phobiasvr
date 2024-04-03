using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonNivel2 : MonoBehaviour
{ 
[Tooltip("If it isn't valve, it can be lever or button (animated)")]
    public bool isLever = false;
[Tooltip("If it is false door can't be used")]
public bool Locked = false;
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
    // Variable para verificar si el jugador está cerca
    public bool playerNear = false;
    void Start()
{
    // Crear un objeto negro que cubra toda la pantalla
    pantallaNegra = new GameObject("PantallaNegra");
    pantallaNegra.transform.parent = Camera.main.transform;
    pantallaNegra.transform.localPosition = Vector3.zero;
    pantallaNegra.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    pantallaNegra.AddComponent<CanvasRenderer>();
    pantallaNegra.AddComponent<UnityEngine.UI.Image>().color = Color.black;

    // Ajustar el tamaño del objeto negro para cubrir toda la pantalla
    RectTransform rectTransform = pantallaNegra.GetComponent<RectTransform>();
    rectTransform.anchorMin = Vector2.zero;
    rectTransform.anchorMax = Vector2.one;
    rectTransform.sizeDelta = Vector2.zero;

    pantallaNegra.SetActive(false);
}

void Update()
{
    if (!Locked && playerNear)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isLever)
            {
                if (anim != null)
                {
                    if (anim.GetBool("LeverUp"))
                        anim.SetBool("LeverUp", false);
                    else
                        anim.SetBool("LeverUp", true);
                }
            }
            else
            {
                if (anim != null)
                    anim.SetTrigger("ButtonPress");

                Debug.Log("Cambio de escena a la numero 2");

                // Activar el oscurecimiento de pantalla
                StartCoroutine(OscurecerYDespuesAclarar());

            }
        }
    }
}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
    IEnumerator OscurecerYDespuesAclarar()
{
    // Activar la pantalla negra
    pantallaNegra.SetActive(true);

    // Oscurecer la pantalla
    float tiempoInicioOscurecimiento = Time.time;
    float duracionOscurecimiento = 2f;

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

    // Esperar 2 segundos antes de aclarar la pantalla automáticamente
    yield return new WaitForSeconds(2f);

        Araña.SetActive(true);
        ObjetoFinal.SetActive(true);
    Revista.SetActive(true);
    Caja.SetActive(true);

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

}
}
