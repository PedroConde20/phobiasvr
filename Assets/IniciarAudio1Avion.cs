using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarAudio1Avion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.audioSources[1].Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
