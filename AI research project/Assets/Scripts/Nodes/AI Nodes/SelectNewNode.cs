using UnityEngine;

public class SelectNewNode : ActionNode
{

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        blackboard.nodeIndex = Random.Range(0, blackboard.nodes.Length);
        return State.Success;
    }
}
