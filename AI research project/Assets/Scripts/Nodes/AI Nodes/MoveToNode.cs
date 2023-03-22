using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNode : ActionNode
{

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        aiController.SetDestination(blackboard.nodes[blackboard.nodeIndex].transform.position);

        if (Vector3.Distance(aiController.gameObject.transform.position, blackboard.nodes[blackboard.nodeIndex].transform.position) < 2)
        {
            blackboard.NextNode();
            return State.Success;
        }
        else
        {
            return State.Running;
        }
    }
}
