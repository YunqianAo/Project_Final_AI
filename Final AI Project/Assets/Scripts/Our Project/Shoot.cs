using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Behavior/Shoot")]
public class Shoot : BasePrimitiveAction
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public override TaskStatus OnUpdate()
    {
        GameObject bullet = Object.Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * 10f; // ×Óµ¯ËÙ¶È
        return TaskStatus.COMPLETED;
    }
}

