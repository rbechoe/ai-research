public class RepeatNode : DecoratorNode
{
    public bool inviteLoop = true;
    public bool crashOnFail = false;
    public int loopCount = 100;

    private int currentCount = 0;
    private bool crashed = false;

    protected override void OnStart()
    {
        currentCount = 0;
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

        State childState = child.Update();

        if (crashOnFail && !crashed)
        {
            if (childState == State.Failure)
            {
                crashed = true;
                return State.Failure;
            }
        }

        if (!inviteLoop)
        {
            if (childState == State.Success || childState == State.Failure)
            {
                currentCount++;

                if (currentCount >= loopCount)
                {
                    return State.Success;
                }
            }
        }

        return State.Running;
    }
}
