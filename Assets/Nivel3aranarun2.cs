using System.Collections;
using UnityEngine;

public class Nivel3aranarun2 : MonoBehaviour
{
    public Animation animacion; // Aseg�rate de asignar tu componente de animaci�n desde el inspector de Unity
    public Transform arana; // La transformaci�n de la ara�a
    public float distanciaMinima = 0.3f; // Distancia m�nima para activar la animaci�n
    public ParticleSystem[] sistemasDeParticulas; // Lista de sistemas de part�culas a considerar
    public string animacionselec;

    void Update()
    {
        // Verificar si la variable "activo" es true y si la animaci�n actual no es "death1"
        if (animacion.clip.name != animacionselec)
        {
            foreach (var sistemaDeParticulas in sistemasDeParticulas)
            {
                if (sistemaDeParticulas == null)
                    continue; // Saltar si el sistema de part�culas es nulo

                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistemaDeParticulas.particleCount];
                int count = sistemaDeParticulas.GetParticles(particles);
                // Iterar sobre cada part�cula en el sistema de part�culas actual
                foreach (var particle in particles)
                {
                    // Calcula la distancia entre la part�cula y la ara�a
                    float distancia = Vector3.Distance(particle.position, arana.position);
                    // Si la distancia es menor que la distancia m�nima especificada, activa la animaci�n
                    if (distancia <= distanciaMinima)
                    {
                        // Si est� dentro del rango, activa la animaci�n
                        animacion.Play(animacionselec);
                        Debug.Log("Ara�a muerta");
                        StartCoroutine(DestruirDespuesDeAnimacion());
                        return; // Termina el bucle ya que hemos encontrado una colisi�n dentro del rango
                    }
                }
            }
        }
    }
    IEnumerator DestruirDespuesDeAnimacion()
    {
        // Esperar hasta que la animaci�n termine
        yield return new WaitForSeconds(animacion[animacionselec].length + 10f);

        // Destruir el objeto
        Destroy(gameObject);
    }
}
