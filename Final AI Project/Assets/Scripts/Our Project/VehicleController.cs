using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 10f;  // 加速度
    public float maxSpeed = 20f;      // 最大速度
    public float turnSpeed = 50f;     // 转向速度
    public float brakeForce = 30f;    // 刹车力度
    public float driftFactor = 0.9f;  // 漂移系数（0~1）

    private float currentSpeed = 0f;  // 当前速度
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down * 0.5f; // 调整重心让车辆更稳定
    }

    // 移动和转向
    public void Move(Vector3 direction, float turnInput)
    {
        // 计算前进
        currentSpeed = Mathf.Clamp(currentSpeed + direction.z * acceleration * Time.deltaTime, -maxSpeed, maxSpeed);

        // 应用前进
        Vector3 forwardMove = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMove);

        // 转向
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turn = turnInput * turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // 漂移
        Vector3 velocity = rb.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        localVelocity.x *= driftFactor;
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    // 刹车功能
    public void Brake()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce * Time.deltaTime);
    }
}
