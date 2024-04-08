using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nivel3aranarun : MonoBehaviour
{
    public GameObject arana; // Asegúrate de asignar tu objeto araña desde el inspector de Unity
    public Animation animacion; // Asegúrate de asignar tu componente de animación desde el inspector de Unity
    public ParticleSystem sistemaDeParticulas;
    public float maxDistance = 1f; // Distancia máxima para considerar la colisión
    void Update()
    {
        // Verificar si la variable "activo" es true y si la animación actual no es "death1"
        if (animacion.clip.name != "death1")
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
                        animacion.Play("death1");
                        Debug.Log("Araña muerta");
                        return; // Termina el bucle ya que hemos encontrado una colisión dentro del rango
                    }
                }
            }
        }
    }

}
