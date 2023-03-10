using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class BehaviorTree : ScriptableObject
{
    public Node rootNode;
    public State treeState = State.Running;
    public List<Node> nodes = new List<Node>();
    public Blackboard blackboard = new Blackboard();

    public State Update()
    {
        if (rootNode.state == State.Running)
        {
            treeState = rootNode.Update();
        }

        return treeState;
    }

    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();

        Undo.RecordObject(this, "Behavior Tree (CreateNode)");
        nodes.Add(node);

        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(node, this);
            Undo.RegisterCreatedObjectUndo(node, "Behavior Tree (CreateNode)");
        }
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node)
    {
        Undo.RecordObject(this, "Behavior Tree (DeleteNode)");
        nodes.Remove(node);

        //AssetDatabase.RemoveObjectFromAsset(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child)
    {
        switch(parent)
        {
            case DecoratorNode:
                Undo.RecordObject((DecoratorNode)parent, "Behavior Tree (AddChild)");
                ((DecoratorNode)parent).child = child;
                child.parent = parent;
                EditorUtility.SetDirty((DecoratorNode)parent);
                break;

            case RootNode:
                Undo.RecordObject((RootNode)parent, "Behavior Tree (AddChild)");
                ((RootNode)parent).child = child;
                child.parent = parent;
                EditorUtility.SetDirty((RootNode)parent);
                break;

            case TypeNode:
                Undo.RecordObject((TypeNode)parent, "Behavior Tree (AddChild)");
                ((TypeNode)parent).child = child;
                if (child.GetType() == typeof(ConditionalNode))
                {
                    ((ConditionalNode)child).typeParents.Add(parent);
                }
                else
                {
                    child.parent = parent;
                }
                EditorUtility.SetDirty((TypeNode)parent);
                break;

            case ControlFlowNode:
                Undo.RecordObject((ControlFlowNode)parent, "Behavior Tree (AddChild)");
                ((ControlFlowNode)parent).children.Add(child);
                child.parent = parent;
                EditorUtility.SetDirty((ControlFlowNode)parent);
                break;
        }
    }

    public void RemoveChild(Node parent, Node child)
    {
        switch (parent)
        {
            case DecoratorNode:
                Undo.RecordObject((DecoratorNode)parent, "Behavior Tree (RemoveChild)");
                ((DecoratorNode)parent).child = null;
                child.parent = null;
                EditorUtility.SetDirty((DecoratorNode)parent);
                break;

            case RootNode:
                Undo.RecordObject((RootNode)parent, "Behavior Tree (RemoveChild)");
                ((RootNode)parent).child = null;
                child.parent = null;
                EditorUtility.SetDirty((RootNode)parent);
                break;

            case TypeNode:
                Undo.RecordObject((TypeNode)parent, "Behavior Tree (RemoveChild)");
                ((TypeNode)parent).child = null;
                if (child.GetType() == typeof(ConditionalNode))
                {
                    ((ConditionalNode)child).typeParents.Remove(parent);
                }
                else
                {
                    child.parent = null;
                }
                EditorUtility.SetDirty((TypeNode)parent);
                break;

            case ControlFlowNode:
                Undo.RecordObject((ControlFlowNode)parent, "Behavior Tree (RemoveChild)");
                ((ControlFlowNode)parent).children.Remove(child);
                child.parent = null;
                EditorUtility.SetDirty((ControlFlowNode)parent);
                break;
        }
    }

    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.child != null)
        {
            children.Add(decorator.child);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode && rootNode.child != null)
        {
            children.Add(rootNode.child);
        }

        TypeNode typeNode = parent as TypeNode;
        if (typeNode && typeNode.child != null)
        {
            children.Add(typeNode.child);
        }

        ControlFlowNode controlFlow = parent as ControlFlowNode;
        if (controlFlow)
        {
            return controlFlow.children;
        }

        return children;
    }

    public void Traverse(Node node, System.Action<Node> visiter)
    {
        if (node)
        {
            visiter.Invoke(node);
            var children = GetChildren(node);
            children.ForEach((n) => Traverse(n, visiter));
        }
    }

    public BehaviorTree Clone()
    {
        BehaviorTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        tree.nodes = new List<Node>();
        Traverse(tree.rootNode, (n) =>
        {
            tree.nodes.Add(n);
        });
        return tree;
    }

    public void Bind(AIController aiController)
    {
        Traverse(rootNode, node =>
        {
            node.aiController = aiController;
            node.blackboard = blackboard;
        });
    }
}
