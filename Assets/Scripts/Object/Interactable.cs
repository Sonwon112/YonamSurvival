using UnityEngine;

public interface Interactable
{
    /// <summary>
    /// �÷��̾� ��ȣ�ۿ� �� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="Player"> �Լ��� ȣ���� Player </param>
    /// <param name="msg"> ��ȣ�ۿ�� �Լ��� �޽��� ���� �ʿ�� ����ϴ� �Ķ���� </param>
    public void Interact(GameObject Player, string msg);
}
