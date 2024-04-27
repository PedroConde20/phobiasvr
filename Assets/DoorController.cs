using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90.0f; // Ángulo de apertura de la puerta
    public bool isOpen = false;    // Variable para rastrear si la puerta está abierta o cerrada
    private Quaternion startRotation; // Rotación inicial de la puerta

    private CambioTextoCanvas cambioTextoCanvas; // Referencia al script CambioTextoCanvas

    void Start()
    {
        // Almacena la rotación inicial de la puerta
        startRotation = transform.rotation;
        // Obtener referencia al script CambioTextoCanvas (asegúrate de que esté en el mismo GameObject o accede de manera adecuada)
        cambioTextoCanvas = GetComponent<CambioTextoCanvas>();
    }

    void Update()
    {
        // Comprueba si se ha presionado el botón "interactuar" del controlador Oculus
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            InteractWithDoor();
        }
    }

    void InteractWithDoor()
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
