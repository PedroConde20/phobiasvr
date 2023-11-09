using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helices : MonoBehaviour
{
    public float rotationSpeed = 1000.0f; // Velocidad de rotación de la hélice.
    public float maxVolume = 1.0f; // Volumen máximo cuando el jugador está cerca.
    public float maxDistance = 10.0f; // Distancia máxima a la que el sonido es audible.

    public AudioSource audioSource;
    public Transform player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.0f; // Inicialmente, el sonido está apagado.
        player = GameObject.FindWithTag("Player").transform; // Encuentra al jugador por su etiqueta.
    }

    void Update()
    {
        // Gira la hélice en su propio eje (eje Z).
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Calcula la distancia entre la hélice y el jugador.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Ajusta el volumen en función de la distancia.
        float volume = Mathf.Lerp(maxVolume, 0.0f, distanceToPlayer / maxDistance);
        audioSource.volume = volume;
    }
}