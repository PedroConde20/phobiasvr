using UnityEngine;

public class SpiderLevel3 : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem sistemaDeParticulas;
    public Transform arana; // La transformaci�n de la ara�a
    public float distanciaMinima = 0.3f; // Distancia m�nima para activar la animaci�n

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistemaDeParticulas.particleCount];
        int count = sistemaDeParticulas.GetParticles(particles);

        foreach (var particle in particles)
        {
            // Calcula la distancia entre la part�cula y la ara�a
            float distancia = Vector3.Distance(particle.position, arana.position);

            // Si la distancia es menor que la distancia m�nima especificada, activa la animaci�n
            if (distancia <= distanciaMinima)
            {
                animator.SetBool("IsDie", true);
                Debug.Log("Ara�a muerta");
                break; // Termina el bucle ya que hemos encontrado una part�cula dentro del rango
            }
        }
    }
}