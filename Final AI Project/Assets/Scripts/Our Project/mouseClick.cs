using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

[Condition("Input/MouseClick")]
public class MouseClick : ConditionBase
{
    [OutParam("clickPosition")]
    public Vector3 clickPosition;

    public override bool Check()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                clickPosition = hit.point;
                return true;
            }
        }
        return false;
    }
}
