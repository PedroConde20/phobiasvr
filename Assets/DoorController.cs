using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90.0f; // Ángulo de apertura de la puerta
    public bool isOpen = false;    // Variable para rastrear si la puerta está abierta o cerrada
    private Quaternion startRotation; // Rotación inicial de la puerta


    void Start()
    {
        // Almacena la rotación inicial de la puerta
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Comprueba si se ha presionado el botón "interactuar" del controlador Oculus
        if (Input.GetButtonDown("Fire1"))
        {
            InteractWithDoor();
        }
    }

    public void InteractWithDoor()
    {
        if (isOpen)
        {
            CloseDoor();
            Debug.Log("ESTA ABIERTA?" + " " + isOpen);
        }
        else
        {
            OpenDoor();
            Debug.Log("ESTA ABIERTA?" + " " + isOpen);
        }
    }

    public void OpenDoor()
    {
        Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
        transform.rotation = targetRotation * startRotation;
        isOpen = true;
    }

    public void CloseDoor()
    {
        transform.rotation = startRotation;
        isOpen = false;
    }
}
