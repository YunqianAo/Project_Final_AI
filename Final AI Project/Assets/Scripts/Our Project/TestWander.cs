using UnityEngine;
using UnityEngine.AI;

public class TestWander : MonoBehaviour
{
    public float minRadius = 5f;
    public float maxRadius = 10f;

    private NavMeshAgent agent;
    private bool needNewTarget = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("No NavMeshAgent found!");
            enabled = false;
            return;
        }

        Debug.Log($"NavMeshAgent found with settings: Speed={agent.speed}, AngularSpeed={agent.angularSpeed}");
    }

    void Update()
    {
        // Si necesitamos un nuevo objetivo o hemos llegado al actual
        if (needNewTarget || !agent.hasPath || agent.remainingDistance < 0.5f)
        {
            FindNewTarget();
        }
    }

    void FindNewTarget()
    {
        // Generar radio aleatorio entre min y max
        float randomRadius = Random.Range(minRadius, maxRadius);

        // Generar dirección aleatoria en el plano XZ
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        randomDirection.Normalize();

        // Calcular posición objetivo
        Vector3 targetPoint = transform.position + randomDirection * randomRadius;

        // Intentar encontrar punto en NavMesh
        if (NavMesh.SamplePosition(targetPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.Log($"New target set at: {hit.position}");
            agent.SetDestination(hit.position);
            needNewTarget = false;

            // Dibujar línea de debug para ver el destino
            Debug.DrawLine(transform.position, hit.position, Color.green, 1.0f);
        }
        else
        {
            Debug.LogWarning("Failed to find valid NavMesh position");
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Dibujar los radios min y max
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        // Si tenemos un destino activo, dibujarlo
        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(agent.destination, 0.5f);
        }
    }
}