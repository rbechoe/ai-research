using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Node : TypeNode
{
    public Vector3 value;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Failure;
    }
}
