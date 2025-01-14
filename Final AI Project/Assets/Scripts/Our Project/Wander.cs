using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Action("Behavior/Wander")]
public class Wander : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("speed")]
    public float speed = 10f;

    private VehicleController vehicleController;
    private Vector3 targetPosition;

    public override void OnStart()
    {
        if (vehicle != null)
        {
            vehicleController = vehicle.GetComponent<VehicleController>();
        }
        ChangeTargetPosition();
    }

    public override TaskStatus OnUpdate()
    {
        if (vehicleController == null) return TaskStatus.FAILED;

        // 控制载具移动
        Vector3 direction = (targetPosition - vehicle.transform.position).normalized;
        vehicleController.Move(direction, 0);

        // 检测是否到达目标点
        if (Vector3.Distance(vehicle.transform.position, targetPosition) < 1f)
        {
            ChangeTargetPosition();
        }
        return TaskStatus.RUNNING;
    }

    private void ChangeTargetPosition()
    {
        // 随机选择目标点
        targetPosition = new Vector3(Random.Range(-10f, 10f), vehicle.transform.position.y, Random.Range(-10f, 10f));
    }
}