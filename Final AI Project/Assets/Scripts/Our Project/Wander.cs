using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Action("Behavior/Wander")]
public class Wander : BasePrimitiveAction
{
    [InParam("agent")] // 动作执行的角色（GameObject）
    public GameObject agent;

    [InParam("moveSpeed")] // 角色的移动速度
    public float moveSpeed;

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
