using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using Pada1.BBCore;

[Action("NavMesh/Gather")]
public class Gather : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("gatherTarget")]
    public Vector3 gatherTarget;

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

        agent.SetDestination(gatherTarget);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            return TaskStatus.COMPLETED;
        }
        return TaskStatus.RUNNING;
    }
}
