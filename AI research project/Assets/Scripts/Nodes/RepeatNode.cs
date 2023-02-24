using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    public bool inviteLoop = true;
    public bool stopOnFail = false;
    public int loopCount = 100;

    private int currentCount = 0;


    protected override void OnStart()
    {
        currentCount = 0;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        State childState = child.Update();

        if (!inviteLoop)
        {
            currentCount++;

            if (currentCount >= loopCount)
            {
                return State.Success;
            }
        }

        if (stopOnFail)
        {
            if (childState == State.Failure)
            {
                return State.Failure;
            }
        }

        return State.Running;
    }
}
