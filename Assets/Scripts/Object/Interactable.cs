using UnityEngine;

public interface Interactable
{
    /// <summary>
    /// 플레이어 상호작용 시 호출되는 함수
    /// </summary>
    /// <param name="Player"> 함수를 호출한 Player </param>
    /// <param name="msg"> 상호작용시 함수로 메시지 전달 필요시 사용하는 파라미터 </param>
    public void Interact(GameObject Player, string msg);
}
