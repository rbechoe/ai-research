using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Debug.Log($"{message}");
        return State.Success;
    }
}
