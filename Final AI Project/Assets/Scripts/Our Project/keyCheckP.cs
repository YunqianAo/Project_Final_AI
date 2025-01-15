using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

[Condition("Check/PatrolCondition")]
public class PatrolCondition : ConditionBase
{
    public override bool Check()
    {
        return Input.GetKey(KeyCode.P);
    }
}
