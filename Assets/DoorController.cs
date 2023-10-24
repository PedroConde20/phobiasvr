using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorController : MonoBehaviour
{
    public float openAngle = 90.0f; // Ángulo de apertura de la puerta
    private bool isOpen = false;    // Variable para rastrear si la puerta está abierta o cerrada
    private Quaternion startRotation; // Rotación inicial de la puerta

    void Start()
    {
        // Almacena la rotación inicial de la puerta
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Comprueba si se ha presionado la tecla "E"
        {
            InteractWithDoor();
        }
    }

    void InteractWithDoor()
    {
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
        transform.rotation = targetRotation * startRotation;
        isOpen = true;
    }

    void CloseDoor()
    {
        transform.rotation = startRotation;
        isOpen = false;
    }
}