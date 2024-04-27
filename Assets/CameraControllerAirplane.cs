using System.Collections;
using UnityEngine;

public class CameraControllerAirplane : MonoBehaviour
{
    public Vector3 originalPosition;

    public Transform trackingSpace; // Referencia al objeto TrackingSpace

    private void Start()
    {
        originalPosition = transform.localPosition;
        // Obtener la referencia al objeto TrackingSpace
        trackingSpace = transform.parent;
    }

    public void ResetCameraPosition()
    {
        // Restablecer la posici�n original del objeto TrackingSpace
        trackingSpace.localPosition = Vector3.zero;
        Debug.Log("C�mara restablecida");
    }

    public void StopShaking()
    {
        // Restablecer la posici�n original del objeto TrackingSpace
        trackingSpace.localPosition = Vector3.zero;
        Debug.Log("Turbulencia de la c�mara detenida");
    }

    public void Shake(float magnitude)
    {
        StartCoroutine(Turbulence(magnitude));
        Debug.Log("Se empez� a mover la c�mara");
    }

    private IEnumerator Turbulence(float magnitude)
    {
        Vector3 startPosition = trackingSpace.localPosition;

        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;

        trackingSpace.localPosition = new Vector3(x, y, startPosition.z);

        yield return null;
    }
}
