using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Behavior/Gather")]
public class Gather : BasePrimitiveAction
{
    public GameObject agent;
    public Transform gatherPoint;
    public float moveSpeed = 5f;

    public override TaskStatus OnUpdate()
    {
        Vector3 direction = (gatherPoint.position - agent.transform.position).normalized;
        agent.transform.position += direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(agent.transform.position, gatherPoint.position) < 1f)
        {
            return TaskStatus.COMPLETED;
        }
        return TaskStatus.RUNNING;
    }
}

