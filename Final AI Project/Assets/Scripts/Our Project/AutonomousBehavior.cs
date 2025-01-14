using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousBehavior : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public Transform gatherPoint;
    public float avoidDistance = 3f;
    public LayerMask avoidLayer;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootInterval = 2f;
    private float shootTimer;
    public int health = 1;

    private Renderer objectRenderer;
    public Color objectColor = Color.white; 
    // Start is called before the first frame update
    void Start()
    {
        shootTimer = shootInterval;
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null){
            objectRenderer.material.color = objectColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        Avoid();
        Gather();
        Shoot();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        // 获取当前巡逻点
        Transform targetPoint = patrolPoints[currentPatrolIndex];

        // 移动到目标点
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 朝目标点旋转
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // 检查是否到达目标点
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void Avoid()
    {
        // 检测周围物体
        Collider[] hits = Physics.OverlapSphere(transform.position, avoidDistance, avoidLayer);
        foreach (var hit in hits)
        {
            // 计算躲避方向
            Vector3 avoidDirection = (transform.position - hit.transform.position).normalized;
            transform.position += avoidDirection * moveSpeed * Time.deltaTime;
        }
    }

    void Gather()
    {
        if (gatherPoint == null) return;

        // 朝聚集点移动
        Vector3 direction = (gatherPoint.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 朝聚集点旋转
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            // 生成子弹
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * 10f; // 子弹速度
            shootTimer = shootInterval;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检测被子弹击中
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--; // 减少血量
        }
    }

    /// <summary>
    /// 设置物体颜色
    /// </summary>
    /// <param name="newColor">新的颜色</param>
    public void SetColor(Color newColor)
    {
        if (objectRenderer != null)
        {
            objectColor = newColor;
            objectRenderer.material.color = objectColor;
        }
    }
}
