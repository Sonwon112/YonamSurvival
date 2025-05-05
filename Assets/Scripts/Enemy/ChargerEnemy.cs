using UnityEngine;

public class ChargingEnemyAI : MonoBehaviour, EnemyInterface, TakeDamage
{
    [SerializeField] public float HP;
    [SerializeField] private float maxPoint;
    public float MoveSpeed { get; set; }

    [Header("기본 이동 설정")]
    public float moveSpeed = 0.7f;
    public float randomness = 0.3f;
    private Vector2 randomOffset;
    private Transform playerTransform;

    [Header("돌진 관련 설정")]
    public float detectionRange = 3f;     // 돌진 감지 거리
    public float chargeForce = 7f;        // 돌진 속도
    public float chargeDuration = 0.4f;   // 돌진 시간
    public float chargeCooldown = 2f;     // 돌진 쿨타임

    private bool isCharging = false;
    private float chargeTimer = 0f;
    private Rigidbody2D rb;
    private DamageComponet damgeComp;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        MoveSpeed = moveSpeed;
        //HP = 100;
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating(nameof(UpdateRandomOffset), 0f, 0.3f);
        damgeComp = GetComponent<DamageComponet>();
        damgeComp.setDamage(5);
        damgeComp.setTarget("Player");
    }

    void Update()
    {
        chargeTimer += Time.deltaTime;

        if (!isCharging)
        {

            // 플레이어가 사정거리 안에 들어왔을때 돌진 스킬 사용 여부
            float distance = Vector2.Distance(transform.position, playerTransform.position);

            if (distance <= detectionRange && chargeTimer >= chargeCooldown)
            {
                Charge();
            }
            else
            {
                Move();
            }
        }
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
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            rb.linearVelocity = direction * MoveSpeed;
        }
    }



    // 돌진 스킬
    private void Charge()
    {
        isCharging = true;
        chargeTimer = 0f;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.linearVelocity = direction * chargeForce;

        damgeComp.setDamage(20);
        // 일정 시간 후 돌진 종료
        Invoke(nameof(EndCharge), chargeDuration);
    }


    // 돌진이 끝났을때 잠깐 멈춘후 다시 움직임
    private void EndCharge()
    {
        isCharging = false;
        rb.linearVelocity = Vector2.zero;
        damgeComp.setDamage(5);
    }

    public void takeDamage(float damage)
    {

        //Debug.Log($"중간몹 피해: {damage} / 남은 HP: {HP}");
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        float point = UnityEngine.Random.Range(1, maxPoint) / 10f;
        GetComponent<SpawnPoint>().spawnPoint(point);
        Destroy(gameObject);
    }

    public void Attack(int atDamage, GameObject target)
    {
        // 충돌 시 데미지 처리
        //Debug.Log($"Lv2 중간몹이 {atDamage}의 데미지를 입혔습니다.");
    }
   



}
