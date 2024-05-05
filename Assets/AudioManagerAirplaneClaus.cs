using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerAirplaneClaus : MonoBehaviour
{
    public AudioSource[] audioSources;

    void Start()
    {
        if (audioSources.Length > 0 && audioSources[0] != null)
        {
            // Establece el primer audio en bucle
            audioSources[0].loop = true;
            // Reproduce el primer audio
            audioSources[0].Play();
        }
        else
        {
            Debug.LogWarning("No se encontraron audioSources asignados o el primer audio es nulo.");
        }
    }

    // Método para reproducir un sonido en un AudioSource específico
    public void PlaySound(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            audioSources[index].Play();
        }
        else
        {
            Debug.LogWarning("Index out of range.");
        }
    }

    // Método para detener todos los sonidos
    public void StopAllSounds()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }
}