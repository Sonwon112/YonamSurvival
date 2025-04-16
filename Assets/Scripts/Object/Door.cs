using UnityEngine;

public class Door : MonoBehaviour, Interactable
{

    [SerializeField] private GameObject guideText;

    public void Interact(GameObject Player)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            guideText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            guideText.SetActive(false);
        }
    }
}
