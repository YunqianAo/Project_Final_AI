using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Action("Behavior/Avoid")]
public class Evade : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("threat")]
    public GameObject threat;

    [InParam("speed")]
    public float speed = 12f;

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
        if (vehicleController == null || threat == null) return TaskStatus.FAILED;

        // º∆À„Ã”±‹∑ΩœÚ
        Vector3 direction = (vehicle.transform.position - threat.transform.position).normalized;
        vehicleController.Move(direction, 0);
        return TaskStatus.RUNNING;
    }
}

