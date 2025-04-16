using UnityEngine;

public class TankEnemy : MonoBehaviour, EnemyInterface
{
    public int HP { get; set; }
    public float MoveSpeed { get; set; }

    [Header("��Ŀ ����")]
    public float moveSpeed = 0.6f;
    public int maxHP = 300;
    public float attackRange = 1.5f; // ���� ����
    public float attackDelay = 1.0f; // ���� �� ���ð�
    public float attackCooldown = 2.0f; // ������ ��Ÿ��
    public int areaDamage = 40; // ���� ������

    private Transform playerTransform;
    private Rigidbody2D myRigid;
    private bool isAttacking = false; // ���� ������ �ƴ��� �Ǵ��ϴ� bool ����

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        HP = maxHP;
        MoveSpeed = moveSpeed;
    }


    // ���� ����� ���� �ֱ����� ������Ʈ �޼��� FixedUpdate()
    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            // �� ������Ʈ�� �÷��̾� ������Ʈ�� �Ÿ� ���
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // ���� �Ǵ�
            if (distanceToPlayer <= attackRange)
            {
                // ���� ������ ������ ���� ����
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


    // �ڷ�ƾ �Լ� ���� �ð� �������� ������ ���� ����
    private System.Collections.IEnumerator AreaAttackRoutine()
    {
        // ���� ���̶�� ���·� ����
        isAttacking = true;


        // ���� ���۽� �������� ���߰� �����Ұ�
        myRigid.linearVelocity = Vector2.zero;
        myRigid.constraints = RigidbodyConstraints2D.FreezeAll;


        // ���� �غ� �ð�
        Debug.Log("��Ŀ ��: ���� �غ� ��...");
        yield return new WaitForSeconds(attackDelay);

        // ���� ���� ����
        Debug.Log("��Ŀ ��: ���� ����!");

        //Tank ������Ʈ�� ���� ��ġ�� �߽����� ���� ���� ��ŭ�� ��ü ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var hit in hits)
        {
            // �����ȿ� �÷��̾ ������
            if (hit.CompareTag("Player"))
            {
                // �÷��̾�� ���� 
                Debug.Log("�÷��̾� ���� ���� ����!");
                Attack(40);

            }

            // �߰��� �ֺ� ������Ʈ�� �Ʊ����Ե� ���� �� �� ����
        }

        // ��Ÿ��
        yield return new WaitForSeconds(attackCooldown);
        myRigid.constraints = RigidbodyConstraints2D.FreezeRotation; // ��ų�� ������ �����Ұ� Ǯ��
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"��Ŀ ����: {damage} / ���� HP: {HP}");

        if (HP <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("��Ŀ ���!");
        Destroy(gameObject);
    }

    public void Attack(int atDamage)
    {
        Debug.Log($"Lv3 ��޸��� {atDamage}�� �������� �������ϴ�.");
    }

    //private void OnDrawGizmosSelected()
    //{
    //    // ���� ���� �ð�ȭ
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}
