using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    public int damage = 20;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = moveDirection * moveSpeed; // ��Ȯ�� ���� �ӵ� ����
    }

    public void SetDirection(Vector2 dir, float speed)
    {
        moveDirection = dir.normalized;
        moveSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyInterface enemy = collision.GetComponent<EnemyInterface>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    // ȭ�� ������ ������ �Ҹ� (������)
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
