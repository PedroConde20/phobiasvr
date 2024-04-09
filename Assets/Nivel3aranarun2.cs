using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel3aranarun2 : MonoBehaviour
{
    public Animation animacion; // Asegúrate de asignar tu componente de animación desde el inspector de Unity
    public List<ParticleSystem> sistemasDeParticulas; // Lista para almacenar todos los sistemas de partículas
    public Transform arana; // La transformación de la araña
    public float distanciaMinima = 1f; // Distancia mínima para activar la animación
    void Start()
    {
        // Inicializar la lista de sistemas de partículas
        sistemasDeParticulas = new List<ParticleSystem>();
    }
    void Update()
    {
        // Verificar si la variable "activo" es true y si la animación actual no es "death1"
        if (animacion.clip.name != "death2")
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
                                // Si está dentro del rango, activa la animación
                                animacion.Play("death2");
                                Debug.Log("Araña muerta");
                                return; // Termina el bucle ya que hemos encontrado una colisión dentro del rango
                            }

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
