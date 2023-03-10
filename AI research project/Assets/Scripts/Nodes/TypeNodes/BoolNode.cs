using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolNode : TypeNode
{
    public bool value;

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
