using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerPasajero : MonoBehaviour
{
    public float prueba = 1f;
    private bool cambioDeEscenaIniciado = false;
    public DatosAntesDeSalir datosAntesDeSalir;
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colision� tiene el tag "Player"
        if (other.CompareTag("Player") && !cambioDeEscenaIniciado)
        {
            cambioDeEscenaIniciado = true;

            // Llama al m�todo para cambiar de escena despu�s de 5 segundos
            Invoke("CambiarEscenaMenu", 1f);
        }

    }
    public void CambiarEscenaMenu()
    {

        // Aqu� cargamos la escena del men� principal
        datosAntesDeSalir.OnApplicationQuit();
        SceneManager.LoadScene("MenuV");
    }
}
