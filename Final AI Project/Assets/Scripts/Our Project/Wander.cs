using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Action("Behavior/Wander")]
public class Wander : BasePrimitiveAction
{
    [InParam("agent")] // ����ִ�еĽ�ɫ��GameObject��
    public GameObject agent;

    [InParam("moveSpeed")] // ��ɫ���ƶ��ٶ�
    public float moveSpeed;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        // ��������ƶ�
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        agent.transform.position += randomDirection.normalized * moveSpeed * Time.deltaTime;
        return TaskStatus.RUNNING;
    }
}
