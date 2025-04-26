using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIManager uiManager;

    [Header("Skill")]
    [SerializeField] private Skill[] skills;

    [Header("Enemy")]
    [SerializeField] private GameObject[] Lv1Enemys;
    [SerializeField] private GameObject[] Lv2Enemys;
    [SerializeField] private GameObject[] Lv3Enemys;


    private Character player;
    private bool isStarting = true;
    private Skill[] playerSkill;

    private float totalTime = 120f;
    private float countdown = 1f;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerGameObject = GameObject.FindWithTag("Player");
        if (playerGameObject == null)
        {
            Debug.Log("Player가 없습니다");
            return;
        }
        player = playerGameObject.GetComponent<Character>();
        uiManager.UpdateTimer(totalTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarting) { 
            DrawSkill();
            isStarting = false;
        }

        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            totalTime -= 1f;       // 1초 단위로 timer 감소
            countdown = 1f;    // countdown 다시 1초로 리셋
            uiManager.UpdateTimer(totalTime);
        }
    }

    public void DrawSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, skills.Length);
            uiManager.setSkills(i, skills[randIndex]);
        }

        uiManager.ShowCard();
        Pause();
    }

    public void SelectedSkill(Skill selectedSkill)
    {
        playerSkill = player.AppendSkill(selectedSkill);
        uiManager.HideCard();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void UpdatePointGauge(float currPoint, float maxGuage)
    {
        uiManager.UpdatePointGauge(currPoint, maxGuage);
    }
}
