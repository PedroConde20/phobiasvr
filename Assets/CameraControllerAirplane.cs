using System.Collections;
using UnityEngine;

public class CameraControllerAirplane : MonoBehaviour
{
    public float initialDuration = 22.0f;
    public float initialMagnitude = 0.05f;

    public float intenseDuration = 15.0f;
    public float maxMagnitude = 0.5f;

    public Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }
    public void ResetCameraPosition()
    {
        // Restablecer la posición original de la cámara
        transform.localPosition = originalPosition;
        Debug.Log("Cámara restablecida");
    }
    public void StopShaking()
    {
        // Restablecer la posición original de la cámara
        transform.localPosition = originalPosition;
        Debug.Log("Turbulencia de la cámara detenida");
    }
    public void Shake(float magnitude)
    {
        StartCoroutine(Turbulence(originalPosition, magnitude));
        Debug.Log("Se empezo a mover la cámara");
        // Fase 1: Turbulencia suave al inicio
        //yield return Turbulence(originalPosition, initialMagnitude, initialDuration);

        // Fase 2: Turbulencia intensa
        //yield return Turbulence(originalPosition, maxMagnitude, intenseDuration);
    }
    private IEnumerator Turbulence(Vector3 startPosition, float magnitude)
    {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, startPosition.z);

            yield return null;
    }
}