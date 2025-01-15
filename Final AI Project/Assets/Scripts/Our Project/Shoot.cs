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
    public bool hasShot; // �����������־����Ƿ����

    public override TaskStatus OnUpdate()
    {
        if (vehicle == null || enemy == null || bulletPrefab == null)
        {
            hasShot = false; // ������ʧ�ܣ�ȷ��״̬����
            return TaskStatus.FAILED;
        }

        // ���������
        Vector3 direction = (enemy.transform.position - vehicle.transform.position).normalized;
        GameObject bullet = Object.Instantiate(bulletPrefab, vehicle.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(direction * shootForce, ForceMode.Impulse);

        hasShot = true; // ����״̬����־������
        return TaskStatus.COMPLETED; // �������״̬
    }
}
