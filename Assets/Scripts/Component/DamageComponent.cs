using UnityEngine;

public class DamageComponet : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private bool isThrowable = false;

    private string target = "Enemy";

    private float currTime = 0f;
    private float term = 3f;

    private void Update()
    {
        currTime += Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currTime < term) return;
        //Debug.Log(collision.name);
        if (collision.gameObject.tag.Equals(target))
        {
            TakeDamage target = collision.gameObject.GetComponent<TakeDamage>();
            target.takeDamage(damage);

            if (isThrowable)
            {
                Destroy(gameObject);
            }
            currTime = 0f;
        }else if (isThrowable && collision.gameObject.tag.Equals("Boundary"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currTime < term) return;
        if (collision.gameObject.tag.Equals(target))
        {
            TakeDamage target = collision.gameObject.GetComponent<TakeDamage>();
            target.takeDamage(damage);

            if (isThrowable)
            {
                Destroy(gameObject);
            }
            currTime = 0;
        }
    }

    /// <summary>
    /// 데미지 지정
    /// </summary>
    /// <param name="damage"> 지정할 데미지 </param>
    public void setDamage(float damage) { this.damage = damage; }

    /// <summary>
    /// 공격할 대상 태그 지정
    /// </summary>
    /// <param name="target"> 지정할 대상 태그 </param>
    public void setTarget(string target) { this.target = target; }

    /// <summary>
    /// 공격 텀 지정
    /// </summary>
    /// <param name="term"> 지정할 텀 </param>
    public void setTerm(float term) { this.term = term; }
}
