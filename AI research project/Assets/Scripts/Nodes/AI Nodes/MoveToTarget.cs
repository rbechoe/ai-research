using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : ActionNode
{
    public float minimalDistance = 2f;
    public float maximalDistance = 20f;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Vector3.Distance(aiController.gameObject.transform.position, blackboard.target.transform.position) < maximalDistance)
        {
            aiController.SetDestination(blackboard.target.transform.position);
        }
        else
        {
            return State.Failure;
        }

        if (Vector3.Distance(aiController.gameObject.transform.position, blackboard.target.transform.position) < minimalDistance)
        {
            aiController.SetDestination(aiController.transform.position);
            aiController.transform.LookAt(blackboard.target.transform.position);
            return State.Success;
        }

        return State.Running;
    }
}
