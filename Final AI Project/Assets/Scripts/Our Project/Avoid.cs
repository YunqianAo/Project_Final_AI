using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Action("Behavior/Avoid")]
public class Avoid : BasePrimitiveAction
{
    public GameObject agent;
    public float avoidDistance = 3f;
    public LayerMask avoidLayer;
    public float moveSpeed = 5f;

    public override TaskStatus OnUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(agent.transform.position, avoidDistance, avoidLayer);
        if (hits.Length > 0)
        {
            Vector3 avoidDirection = (agent.transform.position - hits[0].transform.position).normalized;
            agent.transform.position += avoidDirection * moveSpeed * Time.deltaTime;
            return TaskStatus.RUNNING;
        }
        return TaskStatus.COMPLETED;
    }
}

