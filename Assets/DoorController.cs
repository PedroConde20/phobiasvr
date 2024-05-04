using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90.0f; // �ngulo de apertura de la puerta
    public bool isOpen = false;    // Variable para rastrear si la puerta est� abierta o cerrada
    private Quaternion startRotation; // Rotaci�n inicial de la puerta


    void Start()
    {
        // Almacena la rotaci�n inicial de la puerta
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Comprueba si se ha presionado el bot�n "interactuar" del controlador Oculus
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
