using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConditionalNode : DecoratorNode
{
    public ConditionType conditionType;

    [HideInInspector]
    public List<Node> typeParents = new List<Node>();

    public bool crashOnFalse;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (typeParents.Count != 2)
        {
            description = "Failure: can only have 2 or 3 parental nodes.";
            return State.Failure;
        }

        description = "Success: requirement met.";

        switch (typeParents[0])
        {
            case FloatNode:
                switch (conditionType)
                {
                    case ConditionType.Equal:
                        if (((FloatNode)typeParents[0]).value == ((FloatNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.NotEqual:
                        if (((FloatNode)typeParents[0]).value != ((FloatNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.GreaterThan:
                        if (((FloatNode)typeParents[0]).value > ((FloatNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.LessThan:
                        if (((FloatNode)typeParents[0]).value < ((FloatNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.GreaterThanOrEqual:
                        if (((FloatNode)typeParents[0]).value >= ((FloatNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.LessThanOrEqual:
                        if (((FloatNode)typeParents[0]).value <= ((FloatNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    default:
                        description = "Failure: condition not valid for node type.";
                        return State.Failure;
                }
                break;

            case IntNode:
                switch (conditionType)
                {
                    case ConditionType.Equal:
                        if (((IntNode)typeParents[0]).value == ((IntNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.NotEqual:
                        if (((IntNode)typeParents[0]).value != ((IntNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.GreaterThan:
                        if (((IntNode)typeParents[0]).value > ((IntNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.LessThan:
                        if (((IntNode)typeParents[0]).value < ((IntNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.GreaterThanOrEqual:
                        if (((IntNode)typeParents[0]).value >= ((IntNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.LessThanOrEqual:
                        if (((IntNode)typeParents[0]).value <= ((IntNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    default:
                        description = "Failure: condition not valid for node type.";
                        return State.Failure;
                }
                break;

            case BoolNode:
                switch (conditionType)
                {
                    case ConditionType.Equal:
                        if (((BoolNode)typeParents[0]).value == ((BoolNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.NotEqual:
                        if (((BoolNode)typeParents[0]).value != ((BoolNode)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    default:
                        description = "Failure: condition not valid for node type.";
                        return State.Failure;
                }
                break;

            case Vector3Node:
                switch (conditionType)
                {
                    case ConditionType.Equal:
                        if (((Vector3Node)typeParents[0]).value == ((Vector3Node)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    case ConditionType.NotEqual:
                        if (((Vector3Node)typeParents[0]).value != ((Vector3Node)typeParents[1]).value)
                        {
                            return child.Update();
                        }
                        break;

                    default:
                        description = "Failure: condition not valid for node type.";
                        return State.Failure;
                }
                break;

            default:
                description = "Failure: node type not found.";
                return State.Failure;
        }

        description = "Success: requirement not met.";
        if (crashOnFalse)
        {
            return State.Failure;
        }
        else
        {
            return State.Success;
        }
    }

    public override Node Clone()
    {
        ConditionalNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
