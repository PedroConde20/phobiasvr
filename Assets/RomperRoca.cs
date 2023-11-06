using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomperRoca : MonoBehaviour
{
    public AudioSource sonidoDestruccion; // Agrega una referencia al AudioSource para el sonido de destrucción

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pico"))
        {
            // Reproducir sonido antes de destruir el objeto
            if (sonidoDestruccion != null)
            {
                Debug.Log("Esta con un sonido");
                sonidoDestruccion.Play();
            }

            Destroy(this.gameObject); // Destruye el objeto "Roca" en el que se encuentra el script
        }
    }
}
