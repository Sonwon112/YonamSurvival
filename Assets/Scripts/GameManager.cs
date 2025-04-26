using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIManager uiManager;
    [Header("Skill")]
    [SerializeField] private Skill[] skills;

    private Character player;
    private bool first = true;
    private Skill[] playerSkill;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            DrawSkill();
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
}
