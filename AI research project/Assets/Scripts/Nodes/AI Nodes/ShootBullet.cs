using UnityEngine;

public class ShootBullet : ActionNode
{
    public float shootingRange = 5f;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Vector3.Distance(aiController.transform.position, blackboard.target.transform.position) < shootingRange)
        {
            aiController.transform.LookAt(blackboard.target.transform.position);
            GameObject bullet = Instantiate(blackboard.bulletPrefab, aiController.bulletSpawn.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = aiController.transform.forward * 50;
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
