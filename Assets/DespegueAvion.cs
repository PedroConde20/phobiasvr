using UnityEngine;

public class DespegueAvion : MonoBehaviour
{
    public float actualizar = 1f;
    public float velocidadInicial = 10f; // Velocidad inicial de desplazamiento recto.
    public float velocidadMaxima = 200f; // Velocidad m�xima despu�s de 10 segundos.
    public float inclinacionMaxima = 35f; // Inclinaci�n m�xima de la parte delantera del avi�n.
    public float duracionVueloHorizontal = 10f; // Duraci�n del vuelo horizontal despu�s de los primeros 10 segundos.
    public float duracionInclinacionTotal = 25f; // Duraci�n total de la inclinaci�n.

    public float tiempoTranscurrido = 0f;
    public Quaternion rotacionInicial;
    public bool despegando = false;

    public bool crearpantallanegra = false;
    public GameObject camaritabuscar;
    void Start()
    {
        // Almacenar la rotaci�n inicial del avi�n.
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
                // Mover el avi�n hacia adelante.
                transform.Translate(Vector3.right * velocidadActual * Time.deltaTime);
            }
            else if (tiempoTranscurrido < 10f + duracionInclinacionTotal)
            {
                /*// Calcular el �ngulo de inclinaci�n gradualmente.
                float inclinacionActual = Mathf.Lerp(0f, inclinacionMaxima, (tiempoTranscurrido - 10) / duracionInclinacionTotal);
                // Aplicar la inclinaci�n al avi�n.
                transform.rotation = Quaternion.Euler(-inclinacionActual, rotacionInicial.eulerAngles.y, rotacionInicial.eulerAngles.z);
                // Mover hacia adelante a velocidad m�xima.
                transform.Translate(Vector3.forward * velocidadMaxima * Time.deltaTime);*/
                // Calcular el �ngulo de inclinaci�n gradualmente (inverso).
                float inclinacionActual = Mathf.Lerp(0f, -inclinacionMaxima, (tiempoTranscurrido - 10f) / duracionInclinacionTotal);
                // Aplicar la inclinaci�n al avi�n en el eje Z.
                transform.rotation = Quaternion.Euler(rotacionInicial.eulerAngles.x, rotacionInicial.eulerAngles.y, inclinacionActual);
                // Mover hacia atr�s a velocidad m�xima.
                transform.Translate(Vector3.right * velocidadMaxima * Time.deltaTime);
                // Mover hacia abajo en el eje Y a velocidad m�xima.
                transform.Translate(Vector3.down * velocidadMaxima * Time.deltaTime);
            }
            else
            {
                // Restaurar la rotaci�n inicial del avi�n.
                transform.rotation = rotacionInicial;
                crearpantallanegra = true;
                Debug.Log("Vamos a activar el codigo para la transicion");
                camaritabuscar.GetComponent<TransicionPantallaUsuario>().enabled = true;
            }

            tiempoTranscurrido += Time.deltaTime;

            // Verificar si se ha alcanzado la duraci�n total del despegue.
            if (tiempoTranscurrido >= 10f + duracionInclinacionTotal + duracionVueloHorizontal)
            {
                despegando = false;
            }
        }
    }

}
