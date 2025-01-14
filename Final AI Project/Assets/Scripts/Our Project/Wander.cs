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

        // �����ؾ��ƶ�
        Vector3 direction = (targetPosition - vehicle.transform.position).normalized;
        vehicleController.Move(direction, 0);

        // ����Ƿ񵽴�Ŀ���
        if (Vector3.Distance(vehicle.transform.position, targetPosition) < 1f)
        {
            ChangeTargetPosition();
        }
        return TaskStatus.RUNNING;
    }

    private void ChangeTargetPosition()
    {
        // ���ѡ��Ŀ���
        targetPosition = new Vector3(Random.Range(-10f, 10f), vehicle.transform.position.y, Random.Range(-10f, 10f));
    }
}