using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    private const float defaultTime = 120f;
    private float totalTime = 0f;
    private float countdown = 1f;

    private float enemySpawnTerm = 5f;
    private float[] prevSpawnTime = { 0f, 0f, 0f };

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
        
        totalTime = defaultTime;
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

        SpawnEnemy(Lv1Enemys[0], 0);
        if(defaultTime - totalTime  >= defaultTime * 0.2) {
            SpawnEnemy(Lv2Enemys[0], 1);
        }
        if (defaultTime - totalTime >= defaultTime * 0.7) {
            SpawnEnemy(Lv3Enemys[0], 2);
        }
        
    }

    /// <summary>
    /// 스킬을 고르기 위해서 실행되는 함수
    /// 카드에 표시될 스킬 선정
    /// </summary>
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

    /// <summary>
    /// enemy 스폰 함수
    /// </summary>
    /// <param name="enemy">스폰할 enmey</param>
    /// <param name="index">Lv별 이전에 스폰되었던 시간을 가져오기 위한 index</param>
    public void SpawnEnemy(GameObject enemy, int index)
    {
        if (prevSpawnTime[index] == 0f || prevSpawnTime[index] - totalTime > enemySpawnTerm)
        {
            Vector3 playerPos = player.transform.position;
            float dircetionX = Random.Range(-1f, 2f);
            float dircetionY = Random.Range(-1f, 2f);
            while (dircetionX == 0 && dircetionY == 0) {
                dircetionX = Random.Range(-1f, 2f);
                dircetionY = Random.Range(-1f, 2f);
            }
            float distance = Random.Range(3.5f, 4.5f);

            Vector2 enemyPos = new Vector2(playerPos.x, playerPos.y) + (new Vector2(dircetionX, dircetionY).normalized * distance);

            GameObject enemyTmp = Instantiate(enemy,enemyPos,Quaternion.identity);
            CircleCollider2D enemyCollider = enemyTmp?.GetComponent<CircleCollider2D>();
            enemyCollider.isTrigger = true;

            prevSpawnTime[index] = totalTime;
        }
    }

    /// <summary>
    /// 플레이어가 스킬을 골랐을 때 player의 속성에 skill을 지정 카드 숨기기 위한 함수
    /// </summary>
    /// <param name="selectedSkill"></param>
    public void SelectedSkill(Skill selectedSkill)
    {
        playerSkill = player.AppendSkill(selectedSkill);
        uiManager.HideCard();
    }

    /// <summary>
    /// 일시정저
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// 계속하기
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 포인트 게이지를 업데이트하기 위해서 호출하는 함수
    /// </summary>
    /// <param name="currPoint">현재 포인트</param>
    /// <param name="maxGuage">포인트 최대치</param>
    public void UpdatePointGauge(float currPoint, float maxGuage)
    {
        uiManager.UpdatePointGauge(currPoint, maxGuage);
    }

    /// <summary>
    /// 데미지를 입었을 때 ui를 갱신 시키위해서 호출하는 함수
    /// </summary>
    /// <param name="currHP"> 현재 HP </param>
    /// <param name="maxHP"> 최대 HP </param>
    public void UpdateHPGuage(float currHP, float maxHP)
    {
        uiManager.UpdateHPGuage(currHP, maxHP);
    }

    /// <summary>
    /// 플레이어 사망 시 호출 되는 함수
    /// </summary>
    public void GameOver()
    {
        Pause();
        uiManager.GameOver();
    }

    /// <summary>
    /// 다시하기 버튼 클릭 시
    /// </summary>
    public void Retry() {
        Resume();
        SceneManager.LoadScene(0); // PlayScene 리로드
    }

    /// <summary>
    /// 나가기 버튼 클릭 시
    /// </summary>
    public void StopLevel()
    {
        Debug.Log("메뉴화면으로");
    }
}
