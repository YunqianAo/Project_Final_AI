using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Vehicle/Evade")]
public class Evade : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("enemy")]
    public GameObject enemy;

    public override TaskStatus OnUpdate()
    {
        if (vehicle == null || enemy == null) return TaskStatus.FAILED;

        Vector3 direction = (vehicle.transform.position - enemy.transform.position).normalized;
        vehicle.transform.Translate(direction * Time.deltaTime * 5f);

        return TaskStatus.RUNNING;
    }
}

