using UnityEngine;

public class SpiderLevel3 : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem sistemaDeParticulas;
    public float maxDistance = 1f; // Distancia m�xima para considerar la colisi�n

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistemaDeParticulas.particleCount];
        int count = sistemaDeParticulas.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            RaycastHit[] hits = Physics.RaycastAll(particles[i].position, particles[i].velocity);

            foreach (RaycastHit hit in hits)
            {
                // Comprueba si la colisi�n est� dentro del rango m�ximo
                if (hit.distance <= maxDistance)
                {
                    // Si est� dentro del rango, activa la animaci�n
                    animator.SetBool("IsDie", true);
                    Debug.Log("Ara�a muerta");
                    return; // Termina el bucle ya que hemos encontrado una colisi�n dentro del rango
                }
            }
        }
    }
}
