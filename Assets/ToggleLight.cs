using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public Light luzFoco; // Asigna la luz del foco en el Inspector
    private bool luzEncendida = false;
    public float distanciaActivacion = 5f;

    void Start()
    {
        // Aseg�rate de que la luz est� apagada al inicio
        luzFoco.enabled = luzEncendida;
    }

    void Update()
    {
        // Verifica si el jugador est� cerca y presiona la tecla T
        if (Input.GetKeyDown(KeyCode.T) && PlayerNearby())
        {
            // Cambia el estado de la luz (encendida/apagada)
            luzEncendida = !luzEncendida;

            // Actualiza el estado de la luz
            luzFoco.enabled = luzEncendida;
        }
    }

    bool PlayerNearby()
    {
        // Busca todos los objetos con el tag "Jugador"
        GameObject[] jugadores = GameObject.FindGameObjectsWithTag("Player");

        // Verifica la distancia entre el interruptor y el jugador m�s cercano
        foreach (GameObject jugador in jugadores)
        {
            float distancia = Vector3.Distance(transform.position, jugador.transform.position);
            if (distancia <= distanciaActivacion)
            {
                return true; // El jugador est� lo suficientemente cerca
            }
        }

        return false; // No hay jugadores lo suficientemente cerca
    }
}