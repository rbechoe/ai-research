using System.Collections.Generic;
using UnityEngine;

public abstract class ControlFlowNode : Node
{
    [HideInInspector]
    public List<Node> children = new List<Node>();

    public override Node Clone()
    {
        ControlFlowNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }
}
