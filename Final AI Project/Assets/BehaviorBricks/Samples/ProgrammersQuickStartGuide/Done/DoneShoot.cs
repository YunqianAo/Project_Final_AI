using UnityEngine;
using UnityEngine.AI;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using Pada1.BBCore;

[Action("Combat/AutoShoot")]
public class AutoShoot : BasePrimitiveAction
{
    [InParam("projectilePrefab")]
    public GameObject projectilePrefab;

    [InParam("shooter")]
    public GameObject shooter;

    [InParam("enemy")]
    public GameObject enemy;

    [InParam("fireRate", DefaultValue = 30)]
    public int fireRate;

    [InParam("projectileSpeed", DefaultValue = 20f)]
    public float projectileSpeed;

    [InParam("spawnHeightOffset", DefaultValue = 2f)]
    public float spawnHeightOffset;

    [InParam("rotationSpeed", DefaultValue = 1f)]
    public float rotationSpeed = 1f;

    [InParam("minAngleToShoot", DefaultValue = 5f)]
    public float minAngleToShoot = 5f;

    private int timeSinceLastShot = 0;

    public override TaskStatus OnUpdate()
    {
        if (shooter == null || enemy == null) return TaskStatus.RUNNING;

        // Calcular la dirección hacia el enemigo
        Vector3 directionToEnemy = (enemy.transform.position - shooter.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);

        // Rotar suavemente el tanque hacia el objetivo
        shooter.transform.rotation = Quaternion.Slerp(
            shooter.transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );

        // Calcular el ángulo actual hacia el objetivo
        float currentAngle = Vector3.Angle(shooter.transform.forward, directionToEnemy);

        // Si no estamos lo suficientemente alineados, seguir rotando
        if (currentAngle > minAngleToShoot)
        {
            return TaskStatus.RUNNING;
        }

        // Control de ratio de disparo
        if (fireRate > 0)
        {
            ++timeSinceLastShot;
            timeSinceLastShot %= fireRate;

            if (timeSinceLastShot != 0)
                return TaskStatus.RUNNING;
        }

        if (projectilePrefab != null)
        {
            // Calcular posición de spawn con offset en Y
            Vector3 spawnPosition = shooter.transform.position + new Vector3(0, spawnHeightOffset, 3);

            // Crear la bala usando la rotación actual del tanque
            GameObject bullet = GameObject.Instantiate(projectilePrefab, spawnPosition, shooter.transform.rotation);

            // Configurar Rigidbody
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = bullet.AddComponent<Rigidbody>();
            }

            // Configurar el Rigidbody
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Usar la dirección forward del tanque para el disparo
            rb.velocity = shooter.transform.forward * projectileSpeed;

            return TaskStatus.COMPLETED;
        }

        return TaskStatus.RUNNING;
    }

}