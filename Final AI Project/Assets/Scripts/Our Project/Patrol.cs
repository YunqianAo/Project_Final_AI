using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Behavior/Patrol")]
public class Patrol : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("waypoints")]
    public Transform[] waypoints;

    [InParam("speed")]
    public float speed = 10f;

    private int currentWaypoint = 0;
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
        if (vehicleController == null || waypoints == null || waypoints.Length == 0) return TaskStatus.FAILED;

        // �ƶ�����ǰѲ�ߵ�
        Vector3 direction = (waypoints[currentWaypoint].position - vehicle.transform.position).normalized;
        vehicleController.Move(direction, 0);

        // ����Ŀ�����л�����һ��Ŀ���
        if (Vector3.Distance(vehicle.transform.position, waypoints[currentWaypoint].position) < 1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        return TaskStatus.RUNNING;
    }
}
