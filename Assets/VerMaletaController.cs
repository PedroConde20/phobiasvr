using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VerMaletaController : MonoBehaviour
{
    public Canvas canvas;
    public GameObject objetoVisible;
    public string nuevoTexto = "Ahora entra al avión";

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó tiene el tag "Maleta"
        if (other.CompareTag("Maleta"))
        {
            // Destruye el objeto "VerMaleta"
            Destroy(gameObject);

            // Actualiza el texto del Canvas a "Ahora entra al avión"
            if (canvas != null)
            {
                Text textoCanvas = canvas.GetComponentInChildren<Text>();
                if (textoCanvas != null)
                {
                    textoCanvas.text = nuevoTexto;

                    // Si hay un objeto asignado, hazlo visible
                    if (objetoVisible != null)
                    {
                        objetoVisible.SetActive(true);
                    }
                }
            }
        }
    }
}