using UnityEngine;

public class TankEnemy : MonoBehaviour, EnemyInterface
{
    public int HP { get; set; }
    public float MoveSpeed { get; set; }

    [Header("탱커 설정")]
    public float moveSpeed = 0.6f;
    public int maxHP = 300;
    public float attackRange = 1.5f; // 공격 범위
    public float attackDelay = 1.0f; // 공격 전 대기시간
    public float attackCooldown = 2.0f; // 공격후 쿨타임
    public int areaDamage = 40; // 범위 데미지

    private Transform playerTransform;
    private Rigidbody2D myRigid;
    private bool isAttacking = false; // 공격 중인지 아닌지 판단하는 bool 변수

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        HP = maxHP;
        MoveSpeed = moveSpeed;
    }


    // 물리 계산을 위한 주기적인 업데이트 메서드 FixedUpdate()
    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            // 적 오브젝트와 플레이어 오브젝트의 거리 계산
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // 범위 판단
            if (distanceToPlayer <= attackRange)
            {
                // 공격 범위에 들어오면 공격 시작
                StartCoroutine(AreaAttackRoutine());
            }
            else
            {
                Move();
            }
        }
    }

    public void Move()
    {
        if (playerTransform == null || myRigid == null) return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        myRigid.linearVelocity = direction * MoveSpeed;
    }


    // 코루틴 함수 일정 시간 간격으로 동작을 나눠 실행
    private System.Collections.IEnumerator AreaAttackRoutine()
    {
        // 공격 중이라는 상태로 설정
        isAttacking = true;


        // 공격 시작시 움직임을 멈추고 저지불가
        myRigid.linearVelocity = Vector2.zero;
        myRigid.constraints = RigidbodyConstraints2D.FreezeAll;


        // 공격 준비 시간
        Debug.Log("탱커 적: 공격 준비 중...");
        yield return new WaitForSeconds(attackDelay);

        // 광역 공격 실행
        Debug.Log("탱커 적: 광역 공격!");

        //Tank 오브젝트의 현재 위치를 중심으로 공격 범위 만큼의 객체 감지
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var hit in hits)
        {
            // 범위안에 플레이어가 있을시
            if (hit.CompareTag("Player"))
            {
                // 플레이어에게 피해 
                Debug.Log("플레이어 광역 피해 입음!");
                Attack(40);

            }

            // 추가로 주변 오브젝트나 아군에게도 피해 줄 수 있음
        }

        // 쿨타임
        yield return new WaitForSeconds(attackCooldown);
        myRigid.constraints = RigidbodyConstraints2D.FreezeRotation; // 스킬이 끝나면 저지불가 풀림
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"탱커 피해: {damage} / 남은 HP: {HP}");

        if (HP <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("탱커 사망!");
        Destroy(gameObject);
    }

    public void Attack(int atDamage)
    {
        Debug.Log($"Lv3 상급몹이 {atDamage}의 데미지를 입혔습니다.");
    }

    //private void OnDrawGizmosSelected()
    //{
    //    // 공격 범위 시각화
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}
