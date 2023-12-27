using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public float wanderRadius = 5f;
    public float wanderTimer = 3f;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        // Inicializa el primer destino
        SetNewRandomDestination();
    }

    void Update()
    {
        // Si el agente ha alcanzado el destino o el temporizador ha expirado, elige un nuevo destino
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                SetNewRandomDestination();
                timer = wanderTimer;
            }
        }
    }

    void SetNewRandomDestination()
    {
        // Obtiene una posición aleatoria dentro del radio especificado
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;

        // Establece el nuevo destino para el agente
        agent.SetDestination(finalPosition);
    }
}
