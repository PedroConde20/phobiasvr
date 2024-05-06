using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerAirplaneClaus : MonoBehaviour
{
    public AudioSource[] audioSources;

    public CambioTextoCanvas cambioTextoCanvas;
    public void Start()
    {

    }

    public void Update()
    {
        // Revisar si personSitDown.sonidoAvisoTurbulencia es true
        if (cambioTextoCanvas.sonidoAvisoTurbulencia==true)
        {

            // Reproducir el tercer sonido (índice 1) sin loop
            audioSources[1].Play();

            // Revisar si personSitDown.sonidoTurbulencia es true
            if (cambioTextoCanvas.sonidoTurbulencia== true)
            {
                // Reproducir el primer sonido (índice 2)
                audioSources[2].Play();
            }
            else
            {
                // Si personSitDown.sonidoTurbulencia es false, detener el primer sonido
                audioSources[2].Stop();
            }
        }

    }
}