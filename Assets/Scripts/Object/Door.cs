using System;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{

    [SerializeField] private GameObject guideText;
    [SerializeField] private Transform upperPos;
    [SerializeField] private Transform underPos;

    /// <summary>
    /// �� ��ȣ�ۿ�� �� �� �̵��� ���� �Լ�
    /// </summary>
    /// <param name="Player">��ȣ�ۿ��� �� Player</param>
    /// <param name="msg">�������� �̵����� �Ʒ������� �̵����� �����ϱ� ���� �Ķ����</param>
    public void Interact(GameObject Player, string msg)
    {
        if (msg.Equals("up"))
        {
            if (upperPos != null) {
                Player.transform.position = upperPos.position;
            }
            else
            {
                Debug.Log("�ֻ��� �Դϴ�");
            }
        }
        else if(msg.Equals("down"))
        {
            if (underPos != null)
            {
                Player.transform.position = underPos.position;
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
