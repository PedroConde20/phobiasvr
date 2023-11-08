using UnityEngine;

public class AutoRecto : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad del auto.
    public float tiempoRecto = 4f; // Duración del movimiento recto.

    void Start()
    {
        // Iniciar el movimiento recto.
        GetComponent<Rigidbody>().velocity = transform.forward * velocidad;
    }

    void Update()
    {
        tiempoRecto -= Time.deltaTime;

        if (tiempoRecto <= 0f)
        {
            // Detener el auto cuando se haya alcanzado el tiempo de movimiento recto.
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
