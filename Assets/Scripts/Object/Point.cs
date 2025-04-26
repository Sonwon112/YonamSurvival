using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]private float point = 1f;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void setPoint(float point) {this.point = point; }
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
            Destroy(gameObject);
        }
    }

}
