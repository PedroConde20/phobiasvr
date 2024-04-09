using UnityEngine;

public class SprayController : MonoBehaviour
{
    public ParticleSystem sistemaDeParticulas; // Referencia al Particle System que simula el humo
    public AudioSource sonidoDeSpray; // Referencia al AudioSource del sonido de spray

    bool activado = false; // Variable para rastrear si el spray ha sido activado

    void Start()
    {
        // Detenemos el Particle System y el sonido de spray al inicio
        sistemaDeParticulas.Stop();
        sonidoDeSpray.Stop();
    }

    void Update()
    {
        // Si la tecla F est� siendo presionada y el spray no ha sido activado
        if (Input.GetKeyDown(KeyCode.F) && !activado)
        {
            // Iniciamos el Particle System y el sonido de spray
            sistemaDeParticulas.Play();
            sonidoDeSpray.Play();

            // Marcamos que el spray ha sido activado
            activado = true;

            // Llamamos al m�todo para destruir el GameObject despu�s de 5 segundos
            Destroy(gameObject, 5f);
        }
    }
}
