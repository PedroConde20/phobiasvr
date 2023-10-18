using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSilla : MonoBehaviour
{
    public float distanciaMaxima = 2.0f; // La distancia m�xima para interactuar con la silla
    private GameObject sillaInteractuable; // La silla con la que el jugador puede interactuar

    void Update()
    {
        // Rayo desde la c�mara hacia adelante para detectar objetos con el tag "Silla"
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaMaxima))
        {
            if (hit.collider.CompareTag("Silla"))
            {
                sillaInteractuable = hit.collider.gameObject;
                // Puedes mostrar un mensaje de interacci�n aqu�, por ejemplo, "Presiona E para sentarte".
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SentarseEnSilla();
                }
            }
            else
            {
                sillaInteractuable = null;
            }
        }
        else
        {
            sillaInteractuable = null;
        }
    }

    void SentarseEnSilla()
    {
        // Aqu� puedes implementar la l�gica para que el jugador se siente en la silla.
        if (sillaInteractuable != null)
        {
            // Por ejemplo, puedes desactivar el control del jugador y posicionarlo en la silla.
            // Tambi�n puedes reproducir una animaci�n de sentarse.
        }
    }
}