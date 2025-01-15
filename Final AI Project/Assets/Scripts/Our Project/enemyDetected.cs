using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

[Condition("Detection/EnemyDetected")]
public class EnemyDetected : ConditionBase
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("enemy")]
    public GameObject enemy;

    [InParam("detectionRange")]
    public float detectionRange = 10f;

    public override bool Check()
    {
        if (vehicle == null || enemy == null) return false;

        float distance = Vector3.Distance(vehicle.transform.position, enemy.transform.position);
        return distance <= detectionRange;
    }
}
