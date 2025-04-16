using UnityEngine;

public class RandomImage : MonoBehaviour
{
    [Header("Resource")]
    [SerializeField] private Sprite[] images;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int randIndex = Random.Range(0, images.Length);
        spriteRenderer.sprite = images[randIndex];
    }
}
