using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Range(0f,10f)]
    public float xscalar = 1;
    [Range(0f, 10f)]
    public float yscalar = 1;
    [Range(0f, 100f)]
    public float xspeed;
    [Range(0f, 100f)]
    public float yspeed;

    float xAmount;
    float yAmount;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log($"{Time.time}, {xAmount}, {yAmount}");
    }
    void Update()
    {
        xAmount = Mathf.Cos(Time.time * Mathf.Deg2Rad * xspeed) * xscalar;
        yAmount = Mathf.Sin(Time.time * Mathf.Deg2Rad * yspeed) * yscalar;
        this.transform.position = new Vector3(xAmount, this.transform.position.y, yAmount);

                 }
}
