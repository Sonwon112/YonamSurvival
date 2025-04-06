using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public GameObject Player;
    public float velocity;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Quaternion.FromToRotation(Vector3.up, Player.transform.position - transform.position).eulerAngles.z;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.linearVelocity = transform.up * velocity;
    }
}
