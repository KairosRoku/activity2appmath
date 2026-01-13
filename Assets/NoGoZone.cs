using UnityEngine;
using UnityEngine.SceneManagement;

public class NoGoZone : MonoBehaviour
{
    public Transform player;

    public float warningDistance = 5f;  
    public float dangerDistance = 4f;  

    public float shakeStrength = 0.1f;
    public float shakeSpeed = 20f;

    private SpriteRenderer sr;
    private Color originalColor;
    private Vector3 originalPosition;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        originalPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);


        if (distance <= dangerDistance)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }


        if (distance <= warningDistance)
        {
            sr.color = Color.red;

            float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeStrength;
            transform.position = originalPosition + new Vector3(shakeX, 0f, 0f);
        }
        else
        {
            sr.color = originalColor;
            transform.position = originalPosition;
        }
    }
}