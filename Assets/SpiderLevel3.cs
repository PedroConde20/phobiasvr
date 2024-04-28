using UnityEngine;

public class SpiderLevel3 : MonoBehaviour
{
    public Animator animator;
    public Transform arana; // La transformación de la araña
    public float distanciaMinima = 0.3f; // Distancia mínima para activar la animación
    public ParticleSystem[] sistemasDeParticulas; // Lista de sistemas de partículas a considerar

    void Update()
    {
        foreach (var sistemaDeParticulas in sistemasDeParticulas)
        {
            if (sistemaDeParticulas == null)
                continue; // Saltar si el sistema de partículas es nulo

            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistemaDeParticulas.particleCount];
            int count = sistemaDeParticulas.GetParticles(particles);

            foreach (var particle in particles)
            {
                // Calcula la distancia entre la partícula y la araña
                float distancia = Vector3.Distance(particle.position, arana.position);

                // Si la distancia es menor que la distancia mínima especificada, activa la animación
                if (distancia <= distanciaMinima)
                {
                    animator.SetBool("IsDie", true);
                    Debug.Log("Araña muerta");
                    return; // Termina el método ya que hemos encontrado una partícula dentro del rango
                }
            }
        }
    }
}
