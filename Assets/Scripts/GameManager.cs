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
            Debug.Log("Player�� �����ϴ�");
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
            totalTime -= 1f;       // 1�� ������ timer ����
            countdown = 1f;    // countdown �ٽ� 1�ʷ� ����
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
    /// ��ų�� ���� ���ؼ� ����Ǵ� �Լ�
    /// ī�忡 ǥ�õ� ��ų ����
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
    /// enemy ���� �Լ�
    /// </summary>
    /// <param name="enemy">������ enmey</param>
    /// <param name="index">Lv�� ������ �����Ǿ��� �ð��� �������� ���� index</param>
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
    /// �÷��̾ ��ų�� ����� �� player�� �Ӽ��� skill�� ���� ī�� ����� ���� �Լ�
    /// </summary>
    /// <param name="selectedSkill"></param>
    public void SelectedSkill(Skill selectedSkill)
    {
        playerSkill = player.AppendSkill(selectedSkill);
        uiManager.HideCard();
    }

    /// <summary>
    /// �Ͻ�����
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// ����ϱ�
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// ����Ʈ �������� ������Ʈ�ϱ� ���ؼ� ȣ���ϴ� �Լ�
    /// </summary>
    /// <param name="currPoint">���� ����Ʈ</param>
    /// <param name="maxGuage">����Ʈ �ִ�ġ</param>
    public void UpdatePointGauge(float currPoint, float maxGuage)
    {
        uiManager.UpdatePointGauge(currPoint, maxGuage);
    }

    /// <summary>
    /// �������� �Ծ��� �� ui�� ���� ��Ű���ؼ� ȣ���ϴ� �Լ�
    /// </summary>
    /// <param name="currHP"> ���� HP </param>
    /// <param name="maxHP"> �ִ� HP </param>
    public void UpdateHPGuage(float currHP, float maxHP)
    {
        uiManager.UpdateHPGuage(currHP, maxHP);
    }

    /// <summary>
    /// �÷��̾� ��� �� ȣ�� �Ǵ� �Լ�
    /// </summary>
    public void GameOver()
    {
        Pause();
        uiManager.GameOver();
    }

    /// <summary>
    /// �ٽ��ϱ� ��ư Ŭ�� ��
    /// </summary>
    public void Retry() {
        Resume();
        SceneManager.LoadScene(0); // PlayScene ���ε�
    }

    /// <summary>
    /// ������ ��ư Ŭ�� ��
    /// </summary>
    public void StopLevel()
    {
        Debug.Log("�޴�ȭ������");
    }
}
