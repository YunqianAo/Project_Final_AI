using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Vehicle/Patrol")]
public class Patrol : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("waypoints")]
    public List<Transform> waypoints;

    private int currentWaypointIndex = 0;

    public override TaskStatus OnUpdate()
    {
        if (vehicle == null || waypoints == null || waypoints.Count == 0) return TaskStatus.FAILED;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - vehicle.transform.position).normalized;

        vehicle.transform.Translate(direction * Time.deltaTime * 5f);

        if (Vector3.Distance(vehicle.transform.position, targetWaypoint.position) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }

        return TaskStatus.RUNNING;
    }
}

