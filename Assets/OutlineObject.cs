using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OutlineObject : MonoBehaviour
{
    public float outlineWidth = 0.05f; // Ancho del contorno.
    public Color outlineColor = Color.red; // Color del contorno.

    private Material outlineMaterial;
    private Renderer objectRenderer;
    private bool isPlayerNear = false;

    void Start()
    {
        // Guarda el renderer del objeto.
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            // Crea un material de contorno personalizado.
            outlineMaterial = new Material(objectRenderer.sharedMaterial); // Clona el material actual del objeto.

            // Crea un shader de contorno personalizado y asígnalo al material de contorno.
            Shader outlineShader = Shader.Find("Custom/OutlineShader"); // Reemplaza con el nombre de tu propio shader de contorno.
            if (outlineShader != null)
            {
                outlineMaterial.shader = outlineShader;

                // Configura el material de contorno con el color y el ancho del contorno.
                outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);
                outlineMaterial.SetColor("_OutlineColor", outlineColor);
            }
            else
            {
                Debug.LogError("Shader 'Custom/OutlineShader' not found. Make sure the shader exists.");
            }
        }
        else
        {
            Debug.LogError("No Renderer component found on the object.");
        }
    }

    void Update()
    {
        // Comprueba si el jugador está cerca del objeto (ajusta la distancia a tu necesidad).
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 5f)
        {
            if (!isPlayerNear)
            {
                // Activa el contorno cuando el jugador está cerca.
                outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);
                isPlayerNear = true;
            }
        }
        else
        {
            if (isPlayerNear)
            {
                // Desactiva el contorno cuando el jugador se aleja.
                outlineMaterial.SetFloat("_OutlineWidth", 0f);
                isPlayerNear = false;
            }
        }
    }
}