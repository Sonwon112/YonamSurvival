using UnityEngine;

public class DamageComponet : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Player"))
        {
            TakeDamage target = collision.gameObject.GetComponent<TakeDamage>();
        }
    }

    public void setDamage(float damage) { 
        this.damage = damage;
    }
}
