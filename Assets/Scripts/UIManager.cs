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
    /// ��ųī�忡 ǥ�õ� ��ų�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="index">3�� �� ���°���� index</param>
    /// <param name="skill">������ ��ų</param>
    public void setSkills(int index, Skill skill){skillCards[index].setSkil(skill);}

    /// <summary>
    /// ��ų ī�带 ǥ���ϴ� �Լ�
    /// </summary>
    public void ShowCard(){uiAnimator.SetBool("showCard", true);}

    /// <summary>
    /// ��ų ī�带 ����� �Լ�
    /// </summary>
    public void HideCard(){uiAnimator.SetBool("showCard", false);}

    /// <summary>
    /// ��ų ī�尡 ����°� �Ϸ�Ǿ����� �ִϸ��̼ǿ��� ȣ���ϴ� �Լ�
    /// </summary>
    public void HideDone(){GameManager.Instance.Resume();}

    /// <summary>
    /// �÷��̾��� LevelPoint ��ȯ�� ȭ�� �������ٸ� ���Ž�Ű�� �Լ�
    /// </summary>
    /// <param name="currPoint">���� ����Ʈ</param>
    /// <param name="maxGauge">���� ���������� Max ����Ʈ</param>
    public void UpdatePointGauge(float currPoint, float maxGauge)
    {
        float result = Map(currPoint, 0, maxGauge, GUAGE_UI_MIN, GUAGE_UI_MAX);
        Vector2 currSize = GaugeBar.sizeDelta;
        currSize.x = result;
        GaugeBar.sizeDelta = currSize;
    }

    /// <summary>
    /// Ÿ�̸� �ؽ�Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="currTime">���� Ÿ��</param>
    public void UpdateTimer(float currTime){timerText.text = FormatTime(currTime);}

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    /// <param name="value">���� ��</param>
    /// <param name="fromMin">�Է��ϴ� ���� min</param>
    /// <param name="fromMax">�Է��ϴ� ���� max</param>
    /// <param name="toMin">����ϴ� ���� min</param>
    /// <param name="toMax">����ϴ� ���� max</param>
    /// <returns>���� �� ��°�</returns>
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }

    /// <summary>
    /// �Էµ� Ÿ�̸Ӹ� mm:ss ���·� ��ȯ�ϴ� �޼ҵ�
    /// </summary>
    /// <param name="time">�� ���� float</param>
    /// <returns>mm:ss ������ ���ڿ�</returns>
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
