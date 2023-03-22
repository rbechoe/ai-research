using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform bulletSpawn;
    public GameObject[] nodes;
    public GameObject target;

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

    public void ShootBullet(GameObject _bullet)
    {
        GameObject bullet = Instantiate(_bullet, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 50f;
    }
}
