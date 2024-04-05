using UnityEngine;

public class SprayController : MonoBehaviour
{
    public ParticleSystem sistemaDeParticulas; // Referencia al Particle System que simula el humo
    public AudioSource sonidoDeSpray; // Referencia al AudioSource del sonido de spray

    void Start()
    {
        // Detenemos el Particle System y el sonido de spray al inicio
        sistemaDeParticulas.Stop();
        sonidoDeSpray.Stop();
    }

    void Update()
    {
        // Si la tecla F est� siendo presionada
        if (Input.GetKey(KeyCode.F))
        {
            // Si el Particle System no est� reproduci�ndose, lo iniciamos
            if (!sistemaDeParticulas.isPlaying)
            {
                sistemaDeParticulas.Play();
            }

            // Si el sonido de spray no est� reproduci�ndose, lo iniciamos
            if (!sonidoDeSpray.isPlaying)
            {
                sonidoDeSpray.Play();
            }
        }
        else
        {
            // Si la tecla F no est� siendo presionada, detenemos el Particle System y el sonido de spray
            if (sistemaDeParticulas.isPlaying)
            {
                sistemaDeParticulas.Stop();
            }
            if (sonidoDeSpray.isPlaying)
            {
                sonidoDeSpray.Stop();
            }
        }
    }
}