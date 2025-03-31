using System;
using TMPro.Examples;
using UnityEngine;

public class EnemyAI : MonoBehaviour, EnemyInterface
{
    public int HP { get; set; }
    public float MoveSpeed { get; set; }

    private Transform playerTransform;      // �÷��̾��� Transform
    public float moveSpeed = 0.7f;          // �̵� �ӵ�

    private Vector2 randomOffset;          // ���� �̵��� ���� ������
    private float randomness = 0.3f;       // ���� �������� ����

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        HP = 100;
        MoveSpeed = moveSpeed;

        InvokeRepeating(nameof(UpdateRandomOffset), 0f, 0.3f);  // ���� �̵� ���� ����
    }

    private void Update()
    {
        Move();  // �� �����Ӹ��� �̵�
    }

    private void UpdateRandomOffset()
    {
        randomOffset = UnityEngine.Random.insideUnitCircle * randomness;
    }

    public void Move()
    {
        if (playerTransform != null)
        {
            Vector2 targetPosition = (Vector2)playerTransform.position + randomOffset;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("�� ����!");
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);  // ���� ������ ����
    }

   
}