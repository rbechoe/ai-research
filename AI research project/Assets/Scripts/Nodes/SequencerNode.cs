public class SequencerNode : ControlFlowNode
{
    public bool crashOnFail = false;
    public bool stopAtFirstSuccess = false;

    private int currentChild;
    private bool crashed = false;

    protected override void OnStart()
    {
        currentChild = 0;
        crashed = false;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (crashOnFail && crashed)
        {
            return State.Failure;
        }

        Node child = children[currentChild];

        switch (child.Update())
        {
            case State.Running: 
                return State.Running;
            case State.Failure:
                crashed = true;
                currentChild++;
                if (crashOnFail)
                {
                    return State.Failure;
                }
                break;
            case State.Success:
                currentChild++;
                if (stopAtFirstSuccess)
                {
                    return State.Success;
                }
                break;
        }

        return currentChild == children.Count ? State.Success : State.Running;
    }
}
