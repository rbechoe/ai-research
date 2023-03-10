using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TypeNode : Node
{
    [HideInInspector]
    public Node child;

    public override Node Clone()
    {
        TypeNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
