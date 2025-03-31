using System;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{

    [SerializeField] private GameObject guideText;
    [SerializeField] private Transform UpperPos;
    [SerializeField] private Transform UnderPos;

    /// <summary>
    /// 문 상호작용시 층 수 이동을 위한 함수
    /// </summary>
    /// <param name="Player">상호작용을 한 Player</param>
    /// <param name="msg">위층으로 이동인지 아래층으로 이동인지 구분하기 위한 파라미터</param>
    public void Interact(GameObject Player, string msg)
    {
        if (msg.Equals("up"))
        {
            if (UpperPos != null) {
                Player.transform.position = UpperPos.position;
            }
            else
            {
                Debug.Log("최상층 입니다");
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
                Debug.Log("최하층 입니다");
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
