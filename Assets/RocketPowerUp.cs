using UnityEngine;

public class RocketPowerUp : MonoBehaviour
{
    public float pickupRadius = 0.5f;

    void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= pickupRadius)
        {
            player.IncreaseRocketCount();
            Destroy(gameObject);
        }
    }
}
