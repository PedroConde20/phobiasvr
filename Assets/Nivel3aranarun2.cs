using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel3aranarun2 : MonoBehaviour
{
    public Animation animacion; // Aseg�rate de asignar tu componente de animaci�n desde el inspector de Unity
    public List<ParticleSystem> sistemasDeParticulas; // Lista para almacenar todos los sistemas de part�culas
    public Transform arana; // La transformaci�n de la ara�a
    public float distanciaMinima = 1f; // Distancia m�nima para activar la animaci�n
    void Start()
    {
        // Inicializar la lista de sistemas de part�culas
        sistemasDeParticulas = new List<ParticleSystem>();
    }
    void Update()
    {
        // Verificar si la variable "activo" es true y si la animaci�n actual no es "death1"
        if (animacion.clip.name != "death2")
        {
            // Verificar si la lista de sistemas de part�culas no es nula
            if (sistemasDeParticulas != null)
            {
                // Iterar sobre cada sistema de part�culas en la lista
                foreach (var sistema in sistemasDeParticulas)
                {
                    // Verificar si el sistema de part�culas no es nulo
                    if (sistema != null)
                    {
                        // Obtener todas las part�culas del sistema de part�culas actual
                        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistema.particleCount];
                        int count = sistema.GetParticles(particles);

                        // Iterar sobre cada part�cula en el sistema de part�culas actual
                        foreach (var particle in particles)
                        {
                            // Calcula la distancia entre la part�cula y la ara�a
                            float distancia = Vector3.Distance(particle.position, arana.position);

                            // Si la distancia es menor que la distancia m�nima especificada, activa la animaci�n
                            if (distancia <= distanciaMinima)
                            {
                                // Si est� dentro del rango, activa la animaci�n
                                animacion.Play("death2");
                                Debug.Log("Ara�a muerta");
                                return; // Termina el bucle ya que hemos encontrado una colisi�n dentro del rango
                            }

                        }
                    }
                }
            }
        }
    }

    // M�todo para agregar un sistema de part�culas a la lista
    public void AgregarSistemaDeParticulas(ParticleSystem sistema)
    {
        sistemasDeParticulas.Add(sistema);
    }

    // M�todo para eliminar un sistema de part�culas de la lista
    public void EliminarSistemaDeParticulas(ParticleSystem sistema)
    {
        sistemasDeParticulas.Remove(sistema);
    }
}
