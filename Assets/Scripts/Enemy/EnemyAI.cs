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

    private Rigidbody2D myRigid;



    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        HP = 100;
        MoveSpeed = moveSpeed;
        myRigid = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(UpdateRandomOffset), 0f, 0.3f);  // 랜덤 이동 간격 설정
      

    }

    private void Update()
    {
        Move();  // 매 프레임마다 이동
    }

    // 자연스러운 움직임 
    private void UpdateRandomOffset()
    {
        randomOffset = UnityEngine.Random.insideUnitCircle * randomness;
    }

    public void Move()
    {
        if (playerTransform != null)
        {
            Vector2 targetPosition = (Vector2)playerTransform.position + randomOffset;
            Vector2 forPos = targetPosition - (Vector2)transform.position;

            if (forPos.magnitude > 0.05f)
            {
                Vector2 currentPosition = transform.position; // Vector2로 처리
                myRigid.MovePosition(currentPosition + forPos.normalized * MoveSpeed * Time.fixedDeltaTime);
            }

        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"기본몹 피해: {damage} / 남은 HP: {HP}");
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

    public void Attack(int atDamage)
    {
        Debug.Log($"Lv1 기본몹이 {atDamage}의 데미지를 입혔습니다.");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Attack(5);
        }
    }
}