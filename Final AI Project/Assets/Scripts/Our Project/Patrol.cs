using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Behavior/Patrol")]
public class Patrol : BasePrimitiveAction
{
    public GameObject agent;
    public Transform[] patrolPoints;
    private int currentIndex = 0;
    public float moveSpeed = 5f;

    public override TaskStatus OnUpdate()
    {
        if (patrolPoints.Length == 0) return TaskStatus.FAILED;

        Transform target = patrolPoints[currentIndex];
        Vector3 direction = (target.position - agent.transform.position).normalized;
        agent.transform.position += direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(agent.transform.position, target.position) < 0.5f)
        {
            currentIndex = (currentIndex + 1) % patrolPoints.Length;
        }
        return TaskStatus.RUNNING;
    }
}

