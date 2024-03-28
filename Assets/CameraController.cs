using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxMagnitude = 3.0f;
    public float totalTime = 44.0f;
    public bool finishturbulence = false;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public IEnumerator Shake()
    {
        float elapsed = 0f;
        float currentMagnitude = 0.05f;

        while (elapsed < totalTime)
        {
            float x = Random.Range(-1f, 1f) * currentMagnitude;
            float y = Random.Range(-1f, 1f) * currentMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            // Incrementa la magnitud hasta el máximo en 37 segundos
            if (elapsed < 37.0f)
            {
                currentMagnitude = Mathf.Lerp(0.05f, maxMagnitude, elapsed / 37.0f);
            }
            // Decrementa la magnitud desde el máximo hasta 0 en los últimos 5 segundos
            else
            {
                currentMagnitude = Mathf.Lerp(maxMagnitude, 0.0f, (elapsed - 37.0f) / 5.0f);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restaurar la posición original al finalizar la turbulencia
        transform.localPosition = originalPosition;
        finishturbulence=true;
        Debug.Log("Se termino la turbulencia y la variable esta en true");
    }
}
