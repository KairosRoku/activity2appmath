using System.Net.Sockets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Rocket Firing")]
    public GameObject rocketPrefab;
    public float fireInterval = 2f;
    public int rocketCount = 4;
    public int maxRockets = 8;

    private float fireTimer;

    void Update()
    {
        HandleMovement();
        HandleFiring();
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(x, y).normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    void HandleFiring()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            FireRockets();
            fireTimer = 0f;
        }
    }

    void FireRockets()
    {
        float angleStep = 360f / rocketCount;
        float startAngle = 45f;

        for (int i = 0; i < rocketCount; i++)
        {
            float angle = startAngle + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject rocket = Instantiate(
                rocketPrefab,
                transform.position,
                Quaternion.identity
            );

            rocket.GetComponent<Rocket>().SetDirection(dir);
        }
    }

    public void IncreaseRocketCount()
    {
        rocketCount = Mathf.Min(rocketCount + 1, maxRockets);
    }
}