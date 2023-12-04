using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float initialDuration = 22.0f;
    public float initialMagnitude = 0.05f;

    public float intenseDuration = 15.0f;
    public float maxMagnitude = 0.5f;

    public float finalDuration = 5.0f;
    public float transitionDuration = 1.0f;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public IEnumerator Shake()
    {
        // Fase 1: Turbulencia suave al inicio
        yield return Turbulence(originalPosition, initialMagnitude, initialDuration);

        // Fase 2: Turbulencia intensa
        yield return Turbulence(originalPosition, maxMagnitude, intenseDuration);

        // Espera antes de la transición a la fase final
        yield return new WaitForSeconds(transitionDuration);

        // Fase 3: Turbulencia final (constante)
        yield return ConstantTurbulence(originalPosition, maxMagnitude, finalDuration);

        // Restaurar la posición original al finalizar la turbulencia
        transform.localPosition = originalPosition;
    }

    private IEnumerator Turbulence(Vector3 startPosition, float magnitude, float turbulenceDuration)
    {
        float elapsed = 0f;

        while (elapsed < turbulenceDuration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, startPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ConstantTurbulence(Vector3 startPosition, float magnitude, float turbulenceDuration)
    {
        float elapsed = 0f;

        while (elapsed < turbulenceDuration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, startPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
