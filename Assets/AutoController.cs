using UnityEngine;

public class AutoController : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad del auto.
    public float tiempoRecto = 4f; // Duración del movimiento recto.
    public float tiempoGiro = 3f; // Duración del giro.
    private float tiempoActual = 0f;
    private bool girarIzquierda = true; // Indica si el auto gira a la izquierda o a la derecha.
    private Quaternion objetivoRotacion; // Rotación objetivo durante el giro.
    private Vector3 objetivoPosicion; // Posición objetivo durante el giro.

    void Start()
    {
        // Iniciar el movimiento recto.
        objetivoPosicion = transform.position + transform.forward * velocidad * tiempoRecto;
    }

    void Update()
    {
        tiempoActual += Time.deltaTime;

        if (tiempoActual <= tiempoRecto)
        {
            // Mover el auto hacia adelante durante el tiempoRecto.
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }
        else if (tiempoActual <= tiempoRecto + tiempoGiro)
        {
            // Girar el auto.
            if (girarIzquierda)
            {
                objetivoRotacion = Quaternion.Euler(0, -90, 0);
                objetivoPosicion = transform.position + transform.forward * velocidad * tiempoGiro;
            }
            else
            {
                objetivoRotacion = Quaternion.Euler(0, 90, 0);
                objetivoPosicion = transform.position + transform.forward * velocidad * tiempoGiro;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, objetivoRotacion, Time.deltaTime / tiempoGiro);
            transform.position = Vector3.Lerp(transform.position, objetivoPosicion, Time.deltaTime / tiempoGiro);
        }
        else
        {
            // Cambiar la dirección de giro y reiniciar el tiempo.
            tiempoActual = 0;
            girarIzquierda = !girarIzquierda;
        }
    }
}
