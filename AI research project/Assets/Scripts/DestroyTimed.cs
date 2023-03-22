using UnityEngine;

public class DestroyTimed : MonoBehaviour
{
    public float time = 3f;
    public GameObject effect;

    private void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (effect != null)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
