using UnityEngine;

public class HeallingItem : MonoBehaviour, Item
{
    public float healAmount = 20.0f;

    public void useItem(GameObject target)
    {
        Character CharacterHp = target.GetComponent<Character>();
        if (CharacterHp != null) {
            CharacterHp.upHeal(healAmount);
        }

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            useItem(collision.gameObject);
        }
    }

   
}
