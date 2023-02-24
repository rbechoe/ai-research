using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System;

public class BehaviorTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> { }
    private BehaviorTree tree;

    public BehaviorTreeView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }

    internal void PopulateView(BehaviorTree tree)
    {
        this.tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        tree.nodes.ForEach(n => CreateNodeview(n));
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);
                }
            });
        }

        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        GenerateMenuActions(evt, TypeCache.GetTypesDerivedFrom<ActionNode>());
        GenerateMenuActions(evt, TypeCache.GetTypesDerivedFrom<ControlFlowNode>());
        GenerateMenuActions(evt, TypeCache.GetTypesDerivedFrom<DecoratorNode>());
    }

    private void GenerateMenuActions(ContextualMenuPopulateEvent evt, TypeCache.TypeCollection types)
    {
        foreach (var type in types)
        {
            evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
        }
    }

    private void CreateNode(System.Type type)
    {
        Node node = tree.CreateNode(type);
        CreateNodeview(node);
    }

    private void CreateNodeview(Node node)
    {
        NodeView nodeView = new NodeView(node);
        AddElement(nodeView);
    }
}
