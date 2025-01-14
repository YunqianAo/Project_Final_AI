using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Behavior/Shoot")]
public class Shoot : BasePrimitiveAction
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("enemy")]
    public GameObject enemy;

    [InParam("bulletPrefab")]
    public GameObject bulletPrefab;

    [InParam("shootForce")]
    public float shootForce = 20f;

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
        if (vehicleController == null || bulletPrefab == null || enemy == null)
            return TaskStatus.FAILED;

        // 朝敌人射击
        Vector3 direction = (enemy.transform.position - vehicle.transform.position).normalized;
        //GameObject bullet = Instantiate(bulletPrefab, vehicle.transform.position + vehicle.transform.forward, vehicle.transform.rotation);

        //// 给子弹添加力
        //Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        //if (bulletRb != null)
        //{
        //    bulletRb.AddForce(direction * shootForce, ForceMode.Impulse);
        //}

        return TaskStatus.RUNNING;
    }
}

