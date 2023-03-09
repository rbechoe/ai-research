using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviorTree tree;

    private void Start()
    {
        tree = tree.Clone();
        tree.Bind(GetComponent<AIController>());
    }

    private void Update()
    {
        tree.Update();
    }
}
