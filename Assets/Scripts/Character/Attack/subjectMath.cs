using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class subjectMath : Skill
{
    [SerializeField] private float throwPow = 5f;
    // 공격을 수행하기 위한 변수
    private float attackAreaRadius = 5f;

    private float prevTime = 0f;
    private float currTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startAttack) {
            Attack();
            startAttack = false;
        }
    }

    GameObject findNearEnemy()
    {
        GameObject result = null;
        float minDistance = 0f;
        Collider2D[] aroundCollider = Physics2D.OverlapCircleAll(transform.position, attackAreaRadius, LayerMask.GetMask("Enemy"));
        
        foreach (Collider2D enmey in aroundCollider) {
            float distance = Vector2.Distance(transform.position, enmey.transform.position);
            if (result == null) result = enmey.gameObject;
            else
            {
                if (distance < minDistance) { 
                    result = enmey.gameObject;
                    minDistance = distance;
                }
            }
        }
        return result;
    }

    public override void Attack()
    {
        Debug.Log("수학 연산자 공격");
        GameObject effect = Instantiate(stepEffects[step], transform.position, Quaternion.identity);
        Rigidbody2D rb = effect.GetComponent<Rigidbody2D>();
        DamageComponet damageComponet = effect.GetComponent<DamageComponet>();
        damageComponet?.setDamage(stepDamage[step]);
        damageComponet?.setTarget("Enemy");

        GameObject nearEnemy = findNearEnemy();
        if(nearEnemy == null)
        {
            rb.AddForce(Vector2.right*throwPow, ForceMode2D.Impulse);
        }
        else
        {
            Vector3 direction = (nearEnemy.transform.position - transform.position).normalized;
            Debug.Log(direction);
            rb.AddForce(new Vector2(direction.x, direction.y) * throwPow, ForceMode2D.Impulse);
        }

        Invoke("Attack", stepTerm[step]);
        //rb.AddForce()
    }

    public override void StopAttack()
    {
        base.StopAttack();
        CancelInvoke("Attack");
    }
}
