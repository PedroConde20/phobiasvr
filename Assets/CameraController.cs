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
        float finalMagnitude = maxMagnitude; // Magnitud m�xima de la turbulencia
        float startDecreaseTime = totalTime - 8f; // Tiempo para comenzar a disminuir la magnitud

        while (elapsed < totalTime)
        {
            float x = Random.Range(-1f, 1f) * currentMagnitude;
            float y = Random.Range(-1f, 1f) * currentMagnitude;

            // Mover el objeto TrackingSpace en lugar de la c�mara
            trackingSpace.localPosition = new Vector3(x, y, originalPosition.z);

            // Incrementar la magnitud gradualmente hasta el punto m�ximo
            if (elapsed < startDecreaseTime)
            {
                currentMagnitude = Mathf.Lerp(0.05f, finalMagnitude, elapsed / startDecreaseTime);
            }
            // Decrementar la magnitud gradualmente durante los �ltimos 8 segundos
            else
            {
                currentMagnitude = Mathf.Lerp(finalMagnitude, 0.0f, (elapsed - startDecreaseTime) / 8f);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restaurar la posici�n original del objeto TrackingSpace al finalizar la turbulencia
        trackingSpace.localPosition = originalPosition;
        finishturbulence = true;
        Debug.Log("Se termin� la turbulencia y la variable est� en true");
    }

}