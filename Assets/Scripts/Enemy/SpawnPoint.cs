using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Point")]
    [SerializeField] private Sprite[] pointSprites;
    [SerializeField] private GameObject pointObject;
    [SerializeField] private float oneTimeMaxPoint = 5f;

    public void spawnPoint(float point)
    {
       int count = (int)(point / oneTimeMaxPoint);

        for (int i = 0; i <= count; i++)
        {
            GameObject obj = Instantiate(pointObject, transform.position, Quaternion.identity);
            Point pointComponent = obj?.GetComponentInChildren<Point>();
            
            pointComponent.setPoint(point - oneTimeMaxPoint*(count-i));
            pointComponent.setSprite(pointSprites[0]);
        }
    }
}
