using UnityEngine;

[System.Serializable]
public class Blackboard
{
    public int nodeIndex;
    public float moveSpeed;
    public Vector3 destination;
    public GameObject bulletPrefab;
    public GameObject target;
    public GameObject[] nodes;

    public void NextNode()
    {
        nodeIndex++;
        if (nodeIndex >= nodes.Length)
        {
            nodeIndex = 0;
        }
    }

    public void PrevNode()
    {
        nodeIndex--;
        if (nodeIndex <= 0)
        {
            nodeIndex = nodes.Length - 1;
        }
    }
}
