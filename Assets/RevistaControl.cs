using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevistaControl : MonoBehaviour
{
    private MeshRenderer tapaMeshRenderer;

    private void Start()
    {
        // Obtener el componente MeshRenderer de la tapa
        tapaMeshRenderer = transform.Find("Tapa").GetComponent<MeshRenderer>();

        // Ocultar la tapa al inicio del programa
        tapaMeshRenderer.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Revista"))
        {
            Destroy(other.gameObject);

            // Mostrar la tapa nuevamente
            tapaMeshRenderer.enabled = true;
        }
    }
}