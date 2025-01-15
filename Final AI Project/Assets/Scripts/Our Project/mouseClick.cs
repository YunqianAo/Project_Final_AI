using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

[Condition("Check/MouseClickCondition")]
public class MouseClickCondition : ConditionBase
{
    [OutParam("gatherTarget")]
    public Vector3 gatherTarget;

    public override bool Check()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                gatherTarget = hit.point;
                return true;
            }
        }
        return false;
    }
}
