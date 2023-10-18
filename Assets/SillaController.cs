using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillaController : MonoBehaviour
{
    public float distanciaSilla = 1.5f;
    private bool playerCerca = false;
    private Transform playerTransform;
    private bool sentado = false;
    private CharacterController playerController; // Referencia al CharacterController del jugador.
    public AudioSource audioSource;
    public AudioClip primerClip;
    public AudioClip segundoClip;
    private bool primerClipReproducido = false;

    private void Update()
    {
        if (playerCerca)
        {
            if (!sentado)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Deshabilita temporalmente el CharacterController del jugador.
                    playerController.enabled = false;

                    playerTransform.position = transform.position;
                    playerTransform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                    sentado = true;

                    // Reproduce el primer clip cuando el jugador se sienta.
                    audioSource.clip = primerClip;
                    audioSource.Play();
                    primerClipReproducido = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Habilita el CharacterController del jugador y devuelve al jugador a su posición original.
                    playerController.enabled = true;

                    playerTransform.position = new Vector3(playerTransform.position.x, 0.0f, playerTransform.position.z);
                    sentado = false;
                }
            }
        }

        // Si se ha reproducido el primer clip y han pasado 10 segundos, reproduce el segundo clip.
        if (primerClipReproducido && !audioSource.isPlaying && Time.timeSinceLevelLoad > 10f)
        {
            audioSource.clip = segundoClip;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = true;
            playerTransform = other.transform;
            playerController = other.GetComponent<CharacterController>(); // Obtiene el CharacterController del jugador.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;
        }
    }
}