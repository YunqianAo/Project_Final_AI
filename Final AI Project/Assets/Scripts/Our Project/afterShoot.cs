using Pada1.BBCore.Framework;
using Pada1.BBCore;

[Condition("Combat/AfterShoot")]
public class AfterShoot : ConditionBase
{
    [InParam("hasShot")]
    public bool hasShot;

    public override bool Check()
    {
        return hasShot;
    }
}
