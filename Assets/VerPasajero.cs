using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerPasajero : MonoBehaviour
{
    public float prueba = 1f;
    private bool cambioDeEscenaIniciado = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colision� tiene el tag "Player"
        if (other.CompareTag("Player") && !cambioDeEscenaIniciado)
        {
            cambioDeEscenaIniciado = true;

            // Forzar la persistencia de datos de iluminaci�n
            UnityEditor.Lightmapping.giWorkflowMode = UnityEditor.Lightmapping.GIWorkflowMode.OnDemand;

            // Forzar el bake de la iluminaci�n
            Lightmapping.Clear();
            Lightmapping.Bake();

            // Desactivar la recarga autom�tica de la escena
            SceneManager.LoadScene("Aerofobia", LoadSceneMode.Single);
        }
    }
}
