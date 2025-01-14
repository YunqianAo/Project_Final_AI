using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 10f;  // ���ٶ�
    public float maxSpeed = 20f;      // ����ٶ�
    public float turnSpeed = 50f;     // ת���ٶ�
    public float brakeForce = 30f;    // ɲ������
    public float driftFactor = 0.9f;  // Ư��ϵ����0~1��

    private float currentSpeed = 0f;  // ��ǰ�ٶ�
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down * 0.5f; // ���������ó������ȶ�
    }

    // �ƶ���ת��
    public void Move(Vector3 direction, float turnInput)
    {
        // ����ǰ��
        currentSpeed = Mathf.Clamp(currentSpeed + direction.z * acceleration * Time.deltaTime, -maxSpeed, maxSpeed);

        // Ӧ��ǰ��
        Vector3 forwardMove = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMove);

        // ת��
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turn = turnInput * turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // Ư��
        Vector3 velocity = rb.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        localVelocity.x *= driftFactor;
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    // ɲ������
    public void Brake()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce * Time.deltaTime);
    }
}
