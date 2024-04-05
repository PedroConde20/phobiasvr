using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirAlColisionarConParticleSystem : MonoBehaviour
{
    public ParticleSystem sistemaDeParticulas;

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[sistemaDeParticulas.particleCount];
        int count = sistemaDeParticulas.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(particles[i].position, particles[i].velocity, out hit))
            {
                Destroy(hit.collider.gameObject);
                Debug.Log("Esta destruido");
            }
        }
    }
}
