using System.Collections.Generic;

public abstract class ControlFlowNode : Node
{
    public List<Node> children = new List<Node>();
}
