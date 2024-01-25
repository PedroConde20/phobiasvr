using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonWakeUp : MonoBehaviour
{
    public float distanciaSilla = 1.5f;
    public bool playerCerca = false;
    public Transform playerTransform;
    public bool sentado = false;
    public CharacterController playerController; // Referencia al CharacterController del jugador.

    private bool primeraColision = true; // Variable para controlar la primera colisión.

    private void Update()
    {
        if (playerCerca)
        {
            if (!sentado)
            {
                // La primera vez que colisiona, se sienta automáticamente.
                if (primeraColision)
                {
                    Sentarse();
                    primeraColision = false;
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    // A partir de la segunda vez, el jugador puede levantarse o sentarse al presionar la tecla F.
                    if (sentado)
                    {
                        Pararse();
                    }
                    else
                    {
                        Sentarse();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Pararse();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = true;
            playerTransform = other.transform;
            playerController = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCerca = false;
        }
    }

    private void Sentarse()
    {
        playerController.enabled = false;
        playerTransform.position = transform.position;
        playerTransform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        sentado = true;
    }

    private void Pararse()
    {
        playerController.enabled = true;
        playerTransform.position = new Vector3(playerTransform.position.x, 0.0f, playerTransform.position.z);
        sentado = false;
    }
}
