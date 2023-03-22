using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDestination : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        aiController.SetDestination(blackboard.destination);

        if (Vector3.Distance(aiController.gameObject.transform.position, blackboard.destination) < 2)
        {
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }
}
