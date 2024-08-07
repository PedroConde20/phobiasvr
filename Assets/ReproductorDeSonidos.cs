#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductorDeSonidos : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonido;
    private bool esperando = false;

    void Start()
    {
        // Asegúrate de asignar un AudioSource y un AudioClip en el Inspector de Unity.
        if (audioSource == null || sonido == null)
        {
            Debug.LogError("Asigna un AudioSource y un AudioClip en el Inspector.");
            return;
        }

        // Inicia la reproducción del sonido al principio.
        audioSource.clip = sonido;
        audioSource.Play();

        // Inicia el proceso de espera.
        StartCoroutine(EsperarYReproducir());
    }

    IEnumerator EsperarYReproducir()
    {
        // Espera 10 segundos antes de reproducir el sonido nuevamente.
        yield return new WaitForSeconds(60f);

        // Vuelve a reproducir el sonido.
        audioSource.Play();

        // Reinicia el proceso de espera.
        StartCoroutine(EsperarYReproducir());
    }
}
#endif