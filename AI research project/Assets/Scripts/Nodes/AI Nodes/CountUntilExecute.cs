using UnityEngine;

public class CountUntilExecute : ControlFlowNode
{
    public int ExecuteAtCount = 10;
    public int currentCount = 0;

    private int currentChild;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (currentCount >= ExecuteAtCount)
        {
            Node child = children[currentChild];

            switch (child.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Failure:
                case State.Success:
                    currentChild++;
                    break;
            }

            if (currentChild == children.Count)
            {
                currentCount = 0;
                currentChild = 0;
                return State.Success;
            }
            else
            {
                return State.Running;
            }
        }
        else
        {
            currentCount++;
            return State.Success;
        }
    }
}
