using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Vehicle/Shoot")]
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

    [OutParam("hasShot")]
    public bool hasShot; // 输出参数，标志射击是否完成

    public override TaskStatus OnUpdate()
    {
        if (vehicle == null || enemy == null || bulletPrefab == null)
        {
            hasShot = false; // 如果射击失败，确保状态更新
            return TaskStatus.FAILED;
        }

        // 朝敌人射击
        Vector3 direction = (enemy.transform.position - vehicle.transform.position).normalized;
        GameObject bullet = Object.Instantiate(bulletPrefab, vehicle.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(direction * shootForce, ForceMode.Impulse);

        hasShot = true; // 更新状态，标志射击完成
        return TaskStatus.COMPLETED; // 返回完成状态
    }
}
