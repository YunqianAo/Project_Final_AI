using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Behavior/Gather")]
public class Gather : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("gatherPoint")]
    public Vector3 gatherPoint;

    [InParam("speed")]
    public float speed = 10f;

    private VehicleController vehicleController;

    public override void OnStart()
    {
        if (vehicle != null)
        {
            vehicleController = vehicle.GetComponent<VehicleController>();
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (vehicleController == null) return TaskStatus.FAILED;

        // 移动到鼠标点击位置
        Vector3 direction = (gatherPoint - vehicle.transform.position).normalized;
        vehicleController.Move(direction, 0);
        return TaskStatus.RUNNING;
    }
}
