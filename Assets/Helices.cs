using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helices : MonoBehaviour
{
    public float rotationSpeed = 1000.0f; // Velocidad de rotaci�n de la h�lice.
    public float maxVolume = 1.0f; // Volumen m�ximo cuando el jugador est� cerca.
    public float maxDistance = 10.0f; // Distancia m�xima a la que el sonido es audible.

    public AudioSource audioSource;
    public Transform player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.0f; // Inicialmente, el sonido est� apagado.
        player = GameObject.FindWithTag("Player").transform; // Encuentra al jugador por su etiqueta.
    }

    void Update()
    {
        // Gira la h�lice en su propio eje (eje Z).
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Calcula la distancia entre la h�lice y el jugador.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Ajusta el volumen en funci�n de la distancia.
        float volume = Mathf.Lerp(maxVolume, 0.0f, distanceToPlayer / maxDistance);
        audioSource.volume = volume;
    }
}