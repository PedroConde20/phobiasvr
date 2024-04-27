using UnityEngine;

public class DespegueAvion : MonoBehaviour
{
    public float actualizar = 1f;
    public float velocidadInicial = 10f; // Velocidad inicial de desplazamiento recto.
    public float velocidadMaxima = 200f; // Velocidad máxima después de 10 segundos.
    public float inclinacionMaxima = 35f; // Inclinación máxima de la parte delantera del avión.
    public float duracionVueloHorizontal = 10f; // Duración del vuelo horizontal después de los primeros 10 segundos.
    public float duracionInclinacionTotal = 25f; // Duración total de la inclinación.

    public float tiempoTranscurrido = 0f;
    public Quaternion rotacionInicial;
    public bool despegando = false;

    public bool crearpantallanegra = false;
    public GameObject camaritabuscar;
    void Start()
    {
        // Almacenar la rotación inicial del avión.
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        if (despegando)
        {
            if (tiempoTranscurrido < 10f)
            {
                // Calcular velocidad gradualmente.
                float velocidadActual = Mathf.Lerp(velocidadInicial, velocidadMaxima, tiempoTranscurrido / 10f);
                // Mover el avión hacia adelante.
                transform.Translate(Vector3.right * velocidadActual * Time.deltaTime);
            }
            else if (tiempoTranscurrido < 10f + duracionInclinacionTotal)
            {
                /*// Calcular el ángulo de inclinación gradualmente.
                float inclinacionActual = Mathf.Lerp(0f, inclinacionMaxima, (tiempoTranscurrido - 10) / duracionInclinacionTotal);
                // Aplicar la inclinación al avión.
                transform.rotation = Quaternion.Euler(-inclinacionActual, rotacionInicial.eulerAngles.y, rotacionInicial.eulerAngles.z);
                // Mover hacia adelante a velocidad máxima.
                transform.Translate(Vector3.forward * velocidadMaxima * Time.deltaTime);*/
                // Calcular el ángulo de inclinación gradualmente (inverso).
                float inclinacionActual = Mathf.Lerp(0f, -inclinacionMaxima, (tiempoTranscurrido - 10f) / duracionInclinacionTotal);
                // Aplicar la inclinación al avión en el eje Z.
                transform.rotation = Quaternion.Euler(rotacionInicial.eulerAngles.x, rotacionInicial.eulerAngles.y, inclinacionActual);
                // Mover hacia atrás a velocidad máxima.
                transform.Translate(Vector3.right * velocidadMaxima * Time.deltaTime);
                // Mover hacia abajo en el eje Y a velocidad máxima.
                transform.Translate(Vector3.down * velocidadMaxima * Time.deltaTime);
            }
            else
            {
                // Restaurar la rotación inicial del avión.
                transform.rotation = rotacionInicial;
                crearpantallanegra = true;
                Debug.Log("Vamos a activar el codigo para la transicion");
                camaritabuscar.GetComponent<TransicionPantallaUsuario>().enabled = true;
            }

            tiempoTranscurrido += Time.deltaTime;

            // Verificar si se ha alcanzado la duración total del despegue.
            if (tiempoTranscurrido >= 10f + duracionInclinacionTotal + duracionVueloHorizontal)
            {
                despegando = false;
            }
        }
    }

}
