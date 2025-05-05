using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Point : MonoBehaviour
{
    [SerializeField] private float point = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();   
    }


    public void setPoint(float point) {
        this.point = point;
        int direction = Random.Range(0,2) == 0 ? 1 : -1;
        transform.localScale.Set(Map(point, 1f, 5f, 0.5f, 1.2f)* direction, Map(point, 1f, 5f, 0.5f, 1.2f), Map(point, 1f, 5f, 0.5f, 1.2f));
    }
    public float getPoint(){ return point; }
    public void setSprite(Sprite pointImage)
    {
        spriteRenderer.sprite = pointImage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag.Equals("Point"))
        {
            Character character = collision.GetComponentInParent<Character>();
            character?.appendPoint(point);
            // 효과음 출력 필요
            Destroy(transform.parent.gameObject);
        }
    }

    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        float normalized = (value - fromMin) / (fromMax - fromMin);
        return toMin + normalized * (toMax - toMin);
    }
}
