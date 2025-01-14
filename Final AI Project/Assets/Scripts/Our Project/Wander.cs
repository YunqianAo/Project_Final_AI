using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Action("Behavior/Wander")]
public class Wander : BasePrimitiveAction
{
    public GameObject agent;
    public float moveSpeed = 5f;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        // 随机方向移动
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        agent.transform.position += randomDirection.normalized * moveSpeed * Time.deltaTime;
        return TaskStatus.RUNNING;
    }
}
