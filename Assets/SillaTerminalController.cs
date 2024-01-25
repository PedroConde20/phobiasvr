using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SillaTerminalController : MonoBehaviour
{
    public Canvas canvas;
    public string mensajeNuevo = "Entrega la maleta a recepcion";
    public GameObject objetoVisible;
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Destruye el objeto de la silla
            // Si hay un objeto asignado, hazlo visible
            if (objetoVisible != null)
            {
                Debug.Log("Entro al objeto para que se haga visible");//Se controla de que la colision con el objeto fucnione
                objetoVisible.SetActive(true);
            }
            Destroy(gameObject);

            // Actualiza el texto del Canvas a "Entrega la maleta a recepcion"
            if (canvas != null)
            {
                Text textoCanvas = canvas.GetComponentInChildren<Text>();
                if (textoCanvas != null)
                {
                    textoCanvas.text = mensajeNuevo;
                }
            }
        }
    }
}