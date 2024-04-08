using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nivel3aranarun : MonoBehaviour
{
    public GameObject arana; // Aseg�rate de asignar tu objeto ara�a desde el inspector de Unity
    public Animation animacion; // Aseg�rate de asignar tu componente de animaci�n desde el inspector de Unity
    public ParticleSystem sistemaDeParticulas;
    public float maxDistance = 1f; // Distancia m�xima para considerar la colisi�n
    void Update()
    {
        // Verificar si la variable "activo" es true y si la animaci�n actual no es "death1"
        if (animacion.clip.name != "death1")
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
                        animacion.Play("death1");
                        Debug.Log("Ara�a muerta");
                        return; // Termina el bucle ya que hemos encontrado una colisi�n dentro del rango
                    }
                }
            }
        }
    }

}
