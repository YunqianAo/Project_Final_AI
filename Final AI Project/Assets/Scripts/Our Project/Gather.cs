using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Vehicle/Gather")]
public class Gather : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("targetPosition")]
    public Vector3 targetPosition;

    public override TaskStatus OnUpdate()
    {
        if (vehicle == null) return TaskStatus.FAILED;

        Vector3 direction = (targetPosition - vehicle.transform.position).normalized;
        vehicle.transform.Translate(direction * Time.deltaTime * 5f);

        if (Vector3.Distance(vehicle.transform.position, targetPosition) < 1f)
        {
            return TaskStatus.COMPLETED;
        }

        return TaskStatus.RUNNING;
    }
}