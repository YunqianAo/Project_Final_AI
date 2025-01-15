using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

[Condition("Check/WanderCondition")]
public class WanderCondition : ConditionBase
{
    public override bool Check()
    {
        return Input.GetKey(KeyCode.V);
    }
}
