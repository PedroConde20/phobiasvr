using System.Collections;
using UnityEngine;

public class Nivel3aranarun2 : MonoBehaviour
{
    public Animation animacion; // Asegúrate de asignar tu componente de animación desde el inspector de Unity
    public Transform arana; // La transformación de la araña
    public float distanciaMinima = 0.3f; // Distancia mínima para activar la animación
    public ParticleSystem[] sistemasDeParticulas; // Lista de sistemas de partículas a considerar
    public string animacionselec;

    void Update()
    {
        // Verificar si la variable "activo" es true y si la animación actual no es "death1"
        if (animacion.clip.name != animacionselec)
        {
            foreach (var sistemaDeParticulas in sistemasDeParticulas)
            {
                if (sistemaDeParticulas == null)
                    continue; // Saltar si el sistema de partículas es nulo

                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistemaDeParticulas.particleCount];
                int count = sistemaDeParticulas.GetParticles(particles);
                // Iterar sobre cada partícula en el sistema de partículas actual
                foreach (var particle in particles)
                {
                    // Calcula la distancia entre la partícula y la araña
                    float distancia = Vector3.Distance(particle.position, arana.position);
                    // Si la distancia es menor que la distancia mínima especificada, activa la animación
                    if (distancia <= distanciaMinima)
                    {
                        // Si está dentro del rango, activa la animación
                        animacion.Play(animacionselec);
                        Debug.Log("Araña muerta");
                        StartCoroutine(DestruirDespuesDeAnimacion());
                        return; // Termina el bucle ya que hemos encontrado una colisión dentro del rango
                    }
                }
            }
        }
    }
    IEnumerator DestruirDespuesDeAnimacion()
    {
        // Esperar hasta que la animación termine
        yield return new WaitForSeconds(animacion[animacionselec].length + 10f);

        // Destruir el objeto
        Destroy(gameObject);
    }
}
