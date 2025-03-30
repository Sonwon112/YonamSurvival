using System;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{

    [SerializeField] private GameObject guideText;
    [SerializeField] private Transform UpperPos;
    [SerializeField] private Transform UnderPos;

    public void Interact(GameObject Player, string msg)
    {
        if (msg.Equals("up"))
        {
            if (UpperPos != null) {
                Player.transform.position = UpperPos.position;
            }
            else
            {
                Debug.Log("�ֻ��� �Դϴ�");
            }
        }
        else if(msg.Equals("down"))
        {
            if (UnderPos != null)
            {
                Player.transform.position = UnderPos.position;
            }
            else
            {
                Debug.Log("������ �Դϴ�");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            guideText.SetActive(true);
            Character tmp = collision.gameObject.GetComponent<Character>();
            tmp.setCanInteract(true, this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            guideText.SetActive(false);
            Character tmp = collision.gameObject.GetComponent<Character>();
            tmp.setCanInteract(false, this);
        }
    }
}
