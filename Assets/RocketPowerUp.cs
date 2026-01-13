using UnityEngine;

public class RocketPowerUp : MonoBehaviour
{
    public float pickupRadius = 0.5f;

    void Update()
    {
        RocketSpawner spawner = FindObjectOfType<RocketSpawner>();
        if (spawner == null) return;

        float dist = Vector3.Distance(transform.position, spawner.transform.position);

        if (dist <= pickupRadius)
        {
            spawner.IncreaseRocketCount();
            Destroy(gameObject);
        }
    }
}
