using UnityEngine;

public class SpiderLevel3 : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem sistemaDeParticulas;
    public float maxDistance = 1f; // Distancia máxima para considerar la colisión

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistemaDeParticulas.particleCount];
        int count = sistemaDeParticulas.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            RaycastHit[] hits = Physics.RaycastAll(particles[i].position, particles[i].velocity);

            foreach (RaycastHit hit in hits)
            {
                // Comprueba si la colisión está dentro del rango máximo
                if (hit.distance <= maxDistance)
                {
                    // Si está dentro del rango, activa la animación
                    animator.SetBool("IsDie", true);
                    Debug.Log("Araña muerta");
                    return; // Termina el bucle ya que hemos encontrado una colisión dentro del rango
                }
            }
        }
    }
}
