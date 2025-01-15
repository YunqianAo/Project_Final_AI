using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Action("Vehicle/Wander")]
public class Wander : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("speed")]
    public float speed = 5f;

    private Vector3 targetPosition;

    public override void OnStart()
    {
        SetRandomTarget();
    }

    public override TaskStatus OnUpdate()
    {
        if (vehicle == null) return TaskStatus.FAILED;

        Vector3 direction = (targetPosition - vehicle.transform.position).normalized;
        vehicle.transform.Translate(direction * speed * Time.deltaTime);

        if (Vector3.Distance(vehicle.transform.position, targetPosition) < 1f)
        {
            SetRandomTarget();
        }

        return TaskStatus.RUNNING;
    }

    private void SetRandomTarget()
    {
        targetPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
    }
}
