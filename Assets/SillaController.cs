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

    private void Update()
    {
        if (playerCerca)
        {
            Debug.Log("El Jugador esta cerca");
            if (!sentado)
            {
                Debug.Log("El Jugador esta diferente a sentado");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Deshabilita temporalmente el CharacterController del jugador.
                    playerController.enabled = false;

                    playerTransform.position = transform.position;
                    playerTransform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                    sentado = true;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El Jugador esta cerca en el void ontriggerenter");
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