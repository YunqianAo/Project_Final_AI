using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using Pada1.BBCore;

[Action("NavMesh/Patrol")]
public class Patrol : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("patrolPoints")]
    public Vector3[] patrolPoints;

    private NavMeshAgent agent;
    private int currentPointIndex = 0;

    public override void OnStart()
    {
        if (vehicle != null)
        {
            agent = vehicle.GetComponent<NavMeshAgent>();
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (agent == null || patrolPoints.Length == 0) return TaskStatus.FAILED;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPointIndex]);
        }
        return TaskStatus.RUNNING;
    }
}
