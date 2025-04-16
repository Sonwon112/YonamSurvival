using UnityEngine;

public class DamageComponet : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    private string target = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.tag.Equals(target))
        {
            TakeDamage target = collision.gameObject.GetComponent<TakeDamage>();
            target.takeDamage(damage);

            Destroy(gameObject);
        }
        
    }

    public void setDamage(float damage) { 
        this.damage = damage;
    }

    public void setTarget(string target) { 
        this.target = target;
    }
}
