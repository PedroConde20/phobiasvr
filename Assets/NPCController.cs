using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float velocidad = 2.0f;
    public float distanciaDeteccion = 1.0f; // Distancia para detectar obstáculos
    public float tiempoEspera = 1.0f; // Tiempo de espera después de cambiar de dirección

    private bool cambiandoDireccion = false;

    void Update()
    {
        MoverNPC();
    }

    void MoverNPC()
    {
        if (!cambiandoDireccion)
        {
            // Raycast hacia adelante para detectar obstáculos
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaDeteccion))
            {
                // Si hay un obstáculo, cambiar de dirección y rotar hacia esa dirección
                StartCoroutine(CambiarDireccion());
            }
        }

        // Mover hacia adelante
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    IEnumerator CambiarDireccion()
    {
        // Evitar cambios de dirección simultáneos
        if (cambiandoDireccion)
            yield break;

        cambiandoDireccion = true;

        // Cambiar la dirección del NPC en un ángulo aleatorio
        float nuevoAngulo = Random.Range(90f, 180f);

        // Calcular la rotación deseada
        Quaternion nuevaRotacion = Quaternion.Euler(0, nuevoAngulo, 0);

        // Rotar gradualmente hacia la nueva dirección
        float elapsedTime = 0f;
        float rotationTime = 1.0f; // Tiempo total de rotación

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, nuevaRotacion, (elapsedTime / rotationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(tiempoEspera);

        cambiandoDireccion = false;
    }
}
