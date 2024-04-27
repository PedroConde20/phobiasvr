using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxMagnitude = 0.1f;
    public float totalTime = 44.0f;
    public bool finishturbulence = false;
    private Vector3 originalPosition;

    // Referencia al objeto TrackingSpace
    public Transform trackingSpace;

    private void Start()
    {
        originalPosition = transform.localPosition;
        // Obtener la referencia al objeto TrackingSpacea
        trackingSpace = transform.parent;
    }

    public IEnumerator Shake()
    {
        float elapsed = 0f;
        float currentMagnitude = 0.05f;
        float finalMagnitude = maxMagnitude; // Magnitud máxima de la turbulencia
        float startDecreaseTime = totalTime - 8f; // Tiempo para comenzar a disminuir la magnitud

        while (elapsed < totalTime)
        {
            float x = Random.Range(-1f, 1f) * currentMagnitude;
            float y = Random.Range(-1f, 1f) * currentMagnitude;

            // Mover el objeto TrackingSpace en lugar de la cámara
            trackingSpace.localPosition = new Vector3(x, y, originalPosition.z);

            // Incrementar la magnitud gradualmente hasta el punto máximo
            if (elapsed < startDecreaseTime)
            {
                currentMagnitude = Mathf.Lerp(0.05f, finalMagnitude, elapsed / startDecreaseTime);
            }
            // Decrementar la magnitud gradualmente durante los últimos 8 segundos
            else
            {
                currentMagnitude = Mathf.Lerp(finalMagnitude, 0.0f, (elapsed - startDecreaseTime) / 8f);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restaurar la posición original del objeto TrackingSpace al finalizar la turbulencia
        trackingSpace.localPosition = originalPosition;
        finishturbulence = true;
        Debug.Log("Se terminó la turbulencia y la variable está en true");
    }

}