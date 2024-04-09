using UnityEngine;
using System.Collections.Generic;

public class SpiderLevel3 : MonoBehaviour
{
    public Animator animator;
    public Transform arana; // La transformación de la araña
    public float distanciaMinima = 0.3f; // Distancia mínima para activar la animación

    public List<ParticleSystem> sistemasDeParticulas; // Lista para almacenar todos los sistemas de partículas

    void Start()
    {
        // Inicializar la lista de sistemas de partículas
        sistemasDeParticulas = new List<ParticleSystem>();
    }

    void Update()
    {
        // Verificar si la lista de sistemas de partículas no es nula
        if (sistemasDeParticulas != null)
        {
            // Iterar sobre cada sistema de partículas en la lista
            foreach (var sistema in sistemasDeParticulas)
            {
                // Verificar si el sistema de partículas no es nulo
                if (sistema != null)
                {
                    // Obtener todas las partículas del sistema de partículas actual
                    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistema.particleCount];
                    int count = sistema.GetParticles(particles);

                    // Iterar sobre cada partícula en el sistema de partículas actual
                    foreach (var particle in particles)
                    {
                        // Calcula la distancia entre la partícula y la araña
                        float distancia = Vector3.Distance(particle.position, arana.position);

                        // Si la distancia es menor que la distancia mínima especificada, activa la animación
                        if (distancia <= distanciaMinima)
                        {
                            animator.SetBool("IsDie", true);
                            Debug.Log("Araña muerta");
                            return; // Termina el bucle ya que hemos encontrado una partícula dentro del rango
                        }
                    }
                }
            }
        }
    }

    // Método para agregar un sistema de partículas a la lista
    public void AgregarSistemaDeParticulas(ParticleSystem sistema)
    {
        sistemasDeParticulas.Add(sistema);
    }

    // Método para eliminar un sistema de partículas de la lista
    public void EliminarSistemaDeParticulas(ParticleSystem sistema)
    {
        sistemasDeParticulas.Remove(sistema);
    }
}
