using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : ControlFlowNode
{
    private int currentChild;

    protected override void OnStart()
    {
        currentChild = 0;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Node child = children[currentChild];

        switch (child.Update())
        {
            case State.Running: 
                return State.Running;
            case State.Failure:
                return State.Failure;
            case State.Success:
                currentChild++;
                break;
        }

        return currentChild == children.Count ? State.Success : State.Running;
    }
}
