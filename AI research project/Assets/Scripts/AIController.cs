using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
    }

    public void SetDestination(Vector3 position)
    {
        agent.destination = position;
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
}
