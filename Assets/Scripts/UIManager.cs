using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const float GUAGE_UI_MIN = 60;
    private const float GUAGE_UI_MAX = 1900;

    [Header("SkillCard")]
    [SerializeField] private SkillCard[] skillCards;

    [Header("GuageBar")]
    [SerializeField] private RectTransform GaugeBar;

    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;

    private Animator uiAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        uiAnimator = GetComponent<Animator>();   
    }

    /// <summary>
    /// 스킬카드에 표시될 스킬을 지정하는 함수
    /// </summary>
    /// <param name="index">3개 중 몇번째인지 index</param>
    /// <param name="skill">지정할 스킬</param>
    public void setSkills(int index, Skill skill){skillCards[index].setSkil(skill);}

    /// <summary>
    /// 스킬 카드를 표시하는 함수
    /// </summary>
    public void ShowCard(){uiAnimator.SetBool("showCard", true);}

    /// <summary>
    /// 스킬 카드를 숨기는 함수
    /// </summary>
    public void HideCard(){uiAnimator.SetBool("showCard", false);}

    /// <summary>
    /// 스킬 카드가 숨기는게 완료되었을때 애니메이션에서 호출하는 함수
    /// </summary>
    public void HideDone(){GameManager.Instance.Resume();}

    /// <summary>
    /// 플레이어의 LevelPoint 변환시 화면 게이지바를 갱신시키는 함수
    /// </summary>
    /// <param name="currPoint">현재 포인트</param>
    /// <param name="maxGauge">현재 레벨에서의 Max 포인트</param>
    public void UpdatePointGauge(float currPoint, float maxGauge)
    {
        float result = Map(currPoint, 0, maxGauge, GUAGE_UI_MIN, GUAGE_UI_MAX);
        Vector2 currSize = GaugeBar.sizeDelta;
        currSize.x = result;
        GaugeBar.sizeDelta = currSize;
    }

    /// <summary>
    /// 타이머 텍스트를 갱신하는 함수
    /// </summary>
    /// <param name="currTime">현재 타임</param>
    public void UpdateTimer(float currTime){timerText.text = FormatTime(currTime);}

    /// <summary>
    /// 매핑 함수
    /// </summary>
    /// <param name="value">현재 값</param>
    /// <param name="fromMin">입력하는 값의 min</param>
    /// <param name="fromMax">입력하는 값의 max</param>
    /// <param name="toMin">출력하는 값의 min</param>
    /// <param name="toMax">출력하는 값의 max</param>
    /// <returns>매핑 된 출력값</returns>
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }

    /// <summary>
    /// 입력된 타이머를 mm:ss 형태로 변환하는 메소드
    /// </summary>
    /// <param name="time">초 단위 float</param>
    /// <returns>mm:ss 형태의 문자열</returns>
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
