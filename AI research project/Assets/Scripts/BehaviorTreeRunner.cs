using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    private BehaviorTree tree;

    void Start()
    {
        tree = ScriptableObject.CreateInstance<BehaviorTree>();

        WaitNode wait = ScriptableObject.CreateInstance<WaitNode>();
        DebugLogNode log1 = ScriptableObject.CreateInstance<DebugLogNode>();
        log1.message = "test1";
        DebugLogNode log3 = ScriptableObject.CreateInstance<DebugLogNode>();
        log3.message = "test3";

        SequencerNode sequencer = ScriptableObject.CreateInstance<SequencerNode>();
        sequencer.children.Add(log1);
        sequencer.children.Add(wait);
        sequencer.children.Add(log3);
        sequencer.children.Add(wait);

        RepeatNode loop = ScriptableObject.CreateInstance<RepeatNode>();
        loop.child = sequencer;

        tree.rootNode = loop;
    }

    void Update()
    {
        tree.Update();
    }
}
