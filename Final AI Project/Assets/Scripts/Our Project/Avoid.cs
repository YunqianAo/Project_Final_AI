using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using Pada1.BBCore;

[Action("NavMesh/Evade")]
public class Evade : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("enemy")]
    public GameObject enemy;

    [InParam("evadeDistance")]
    public float evadeDistance = 10f;

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
        if (agent == null || enemy == null) return TaskStatus.FAILED;

        Vector3 evadeDirection = (vehicle.transform.position - enemy.transform.position).normalized;
        Vector3 evadeTarget = vehicle.transform.position + evadeDirection * evadeDistance;

        if (NavMesh.SamplePosition(evadeTarget, out NavMeshHit hit, evadeDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            return TaskStatus.RUNNING;
        }
        return TaskStatus.FAILED;
    }
}
