using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductorSonido2 : MonoBehaviour
{
    public AudioSource audioSourceToControl;
    public AudioClip sonido;
    private bool esperando = false;

    void Start()
    {
        // Asegúrate de asignar un AudioSource y un AudioClip en el Inspector de Unity.
        if (audioSourceToControl == null || sonido == null)
        {
            Debug.LogError("Asigna un AudioSource y un AudioClip en el Inspector.");
            return;
        }

        // Inicia el proceso de espera.
        StartCoroutine(EsperarYReproducir());
    }

    IEnumerator EsperarYReproducir()
    {
        // Espera 10 segundos antes de reproducir el sonido.
        yield return new WaitForSeconds(180);

        // Vuelve a reproducir el sonido en el AudioSource específico.
        audioSourceToControl.clip = sonido;
        audioSourceToControl.Play();

        // Reinicia el proceso de espera.
        StartCoroutine(EsperarYReproducir());
    }
}