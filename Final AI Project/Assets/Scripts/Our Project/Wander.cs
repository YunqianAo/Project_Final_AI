using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using Pada1.BBCore;

[Action("NavMesh/Wander")]
public class Wander : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("wanderRadius")]
    public float wanderRadius = 10f;

    private NavMeshAgent agent;

    public override void OnStart()
    {
        if (vehicle != null)
        {
            agent = vehicle.GetComponent<NavMeshAgent>();
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (agent == null) return TaskStatus.FAILED;

        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += vehicle.transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            return TaskStatus.RUNNING;
        }
        return TaskStatus.FAILED;
    }
}
