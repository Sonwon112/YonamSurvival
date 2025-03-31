using System;
using TMPro.Examples;
using UnityEngine;

public class EnemyAI : MonoBehaviour, EnemyInterface
{
    public int HP { get; set; }
    public float MoveSpeed { get; set; }

    private Transform playerTransform;      // 플레이어의 Transform
    public float moveSpeed = 0.7f;          // 이동 속도

    private Vector2 randomOffset;          // 랜덤 이동을 위한 오프셋
    private float randomness = 0.3f;       // 랜덤 움직임의 정도

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        HP = 100;
        MoveSpeed = moveSpeed;

        InvokeRepeating(nameof(UpdateRandomOffset), 0f, 0.3f);  // 랜덤 이동 간격 설정
    }

    private void Update()
    {
        Move();  // 매 프레임마다 이동
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
        Debug.Log("적 공격!");
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);  // 적이 죽으면 삭제
    }

   
}