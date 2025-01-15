using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using Pada1.BBCore;
using BBUnity.Actions;

[Action("MyActions/Wander")]
public class WanderAction : GOAction
{
    [InParam("minRadius", DefaultValue = 5f)]
    public float minRadius = 5f;

    [InParam("maxRadius", DefaultValue = 10f)]
    public float maxRadius = 10f;

    private NavMeshAgent agent;
    private bool needNewTarget = true;

    public override void OnStart()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("No NavMeshAgent found!");
            return;
        }
        Debug.Log($"NavMeshAgent found with settings: Speed={agent.speed}, AngularSpeed={agent.angularSpeed}");
        // Buscar el primer objetivo al iniciar
        FindNewTarget();
    }

    public override TaskStatus OnUpdate()
    {
        // Si no hay agente, fallar
        if (agent == null)
            return TaskStatus.FAILED;

        // Si hemos llegado al destino o necesitamos nuevo objetivo
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            needNewTarget = true;
        }

        // Si necesitamos nuevo objetivo, buscarlo
        if (needNewTarget)
        {
            FindNewTarget();
        }

        // Siempre retornamos RUNNING para que la acción continúe
        return TaskStatus.RUNNING;
    }

    private void FindNewTarget()
    {
        // Generar radio aleatorio entre min y max
        float randomRadius = Random.Range(minRadius, maxRadius);

        // Generar dirección aleatoria en el plano XZ
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        randomDirection.Normalize();

        // Calcular posición objetivo
        Vector3 targetPoint = gameObject.transform.position + randomDirection * randomRadius;

        // Intentar encontrar punto en NavMesh
        if (NavMesh.SamplePosition(targetPoint, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
        {
            Debug.Log($"New target set at: {hit.position}");
            agent.SetDestination(hit.position);
            needNewTarget = false;

            // Dibujar línea de debug para ver el destino
            Debug.DrawLine(gameObject.transform.position, hit.position, Color.green, 1.0f);
        }
        else
        {
            Debug.LogWarning("Failed to find valid NavMesh position");
            needNewTarget = true;
        }
    }

    public override void OnAbort()
    {
        if (agent != null)
        {
            agent.ResetPath();
        }
    }
}