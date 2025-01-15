using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

[Condition("Input/KeyCheck")]
public class KeyCheck : ConditionBase
{
    [InParam("key")]
    public KeyCode key;

    public override bool Check()
    {
        return Input.GetKey(key);
    }
}
