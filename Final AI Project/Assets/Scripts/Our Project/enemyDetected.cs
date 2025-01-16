using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

[Condition("Check/EnemyDetected")]
public class EnemyDetected : ConditionBase
{
    [InParam("vehicle")]
    public GameObject vehicle;

    [InParam("enemy")]
    public GameObject enemy;

    [InParam("detectionRange")]
    public float detectionRange = 1000f;

    public override bool Check()
    {
        return Vector3.Distance(vehicle.transform.position, enemy.transform.position) <= detectionRange;
    }
}

