using UnityEngine;

public class Point : MonoBehaviour
{
    private float level = 1f;
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


    public void setLevel(float level){this.level = level;}
    public float getLevel(){ return level; }
    public void setSprite(Sprite pointImage)
    {
        spriteRenderer.sprite = pointImage;
    }

}
