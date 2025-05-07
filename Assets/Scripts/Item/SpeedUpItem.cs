using UnityEngine;

public class SpeedUpItem : MonoBehaviour , Item
{

    // 버프 추가 이속 및 지속시간
    public float upSpeed = 3f;
    public float duration = 5f;

    public void useItem(GameObject target)
    {
        Character CharacterSpeed = target.GetComponent<Character>();
        if (CharacterSpeed != null) { 
           CharacterSpeed.ApplySpeedBoost(upSpeed , duration);
        }

        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            useItem(collision.gameObject);
        }
    }

    
}
