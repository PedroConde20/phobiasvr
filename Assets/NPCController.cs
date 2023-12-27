using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float velocidad = 2.0f;
    public float distanciaDeteccion = 1.0f; // Distancia para detectar obst�culos
    public float tiempoEspera = 1.0f; // Tiempo de espera despu�s de cambiar de direcci�n

    private bool cambiandoDireccion = false;

    void Update()
    {
        MoverNPC();
    }

    void MoverNPC()
    {
        if (!cambiandoDireccion)
        {
            // Raycast hacia adelante para detectar obst�culos
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaDeteccion))
            {
                // Si hay un obst�culo, cambiar de direcci�n y rotar hacia esa direcci�n
                StartCoroutine(CambiarDireccion());
            }
        }

        // Mover hacia adelante
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    IEnumerator CambiarDireccion()
    {
        // Evitar cambios de direcci�n simult�neos
        if (cambiandoDireccion)
            yield break;

        cambiandoDireccion = true;

        // Cambiar la direcci�n del NPC en un �ngulo aleatorio
        float nuevoAngulo = Random.Range(90f, 180f);

        // Calcular la rotaci�n deseada
        Quaternion nuevaRotacion = Quaternion.Euler(0, nuevoAngulo, 0);

        // Rotar gradualmente hacia la nueva direcci�n
        float elapsedTime = 0f;
        float rotationTime = 1.0f; // Tiempo total de rotaci�n

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
