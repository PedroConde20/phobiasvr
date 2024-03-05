using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambioTextoCanvas : MonoBehaviour
{
    public Text textoCanvas;
    public PersonSitDown personSitDown; // Referencia al script PersonSitDown
    private void Start()
    {
        StartCoroutine(EsperarYCambiarTexto());
    }

    private IEnumerator EsperarYCambiarTexto()
    {
        yield return new WaitForSeconds(10f);

        if (textoCanvas != null)
        {
            textoCanvas.text = "Deberías ir al baño, en unos minutos el avión aterrizará.";
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

        // Esperar 3 segundos después de que pantallaNegraActiva sea true
        yield return new WaitForSeconds(3f);

        // Cambiar el texto del canvas
        textoCanvas.text = "Oh... algo anda mal allá afuera, tal vez debas pararte con F e investigar.";
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