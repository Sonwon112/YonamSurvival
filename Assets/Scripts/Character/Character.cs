using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, TakeDamage
{
    [Header("State Variable")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float runSpeed = 5.5f;
    [SerializeField] private float diagonalTmp = 0.7f;
    [SerializeField] private float rollPow = 1f;

   

    private Rigidbody2D rbCharacter;
    private Animator animator;


    // �÷��̾����
    private float direction = 1;
    private bool isRoll = false;

    private float maxHP = 100f;
    private float HP = 100f;

    private int level = 0;
    private float gauge = 0f;
    private float maxGauge = 10f;
    private float gaugeInterval = 20f;
    
    // ��ų
    private Skill[] haveSkill = new Skill[5];
    private GameObject[] skillInstance = new GameObject[5];
    private int currIndex = 0;

    // ��ȣ�ۿ�
    private bool canInteract;
    private Interactable interactTarget;

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, 3);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbCharacter = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float isRun = Input.GetAxis("Run");
        float tmpSpeed = isRun > 0 ? runSpeed : speed;

        animator.SetFloat("RunSpeed", tmpSpeed);

        if (horizontal != 0)
        {
            direction = horizontal < 0 ? -1 : 1;
            transform.localScale = new Vector3(direction, 1, 1);
        }

        float absHor = Mathf.Abs(horizontal);
        float absVert = Mathf.Abs(vertical);

        float movingSpeed = absHor + absVert > 1.1 ? tmpSpeed * diagonalTmp : tmpSpeed;

        Vector2 movingDirection = new Vector2(horizontal,vertical).normalized;

        Vector2 currRollPow = isRoll ? movingDirection*rollPow : Vector2.zero;

        //Debug.Log(isRoll);

        rbCharacter.linearVelocity = new Vector2(horizontal * movingSpeed, vertical * movingSpeed) + currRollPow;

        animator.SetFloat("speed", absHor + absVert);

        
    }

    // Update is called once per frame
    void Update()
    {
        // ������
        if (Input.GetButtonDown("Jump") && !isRoll)
        {
            //isRoll = true;
            animator.SetBool("roll", true);
            Invoke("setRollTrue", 0.1f);
        }
        // ��ȣ�ۿ�
        if (canInteract)
        {
            // ���� �̵�
            if (Input.GetButtonDown("MoveUpper"))
            {
                interactTarget.Interact(gameObject, "up");
            }else if (Input.GetButtonDown("MoveUnder"))
            {
                interactTarget.Interact(gameObject, "down");
            }
        }

    }

    /// <summary>
    /// ��ȣ�ۿ� ��ü ���� / Ż�� �� ��ȣ�ۿ� ���� ����
    /// </summary>
    /// <param name="canInteract"></param>
    /// <param name="interactTarget"></param>
    public void setCanInteract(bool canInteract, Interactable interactTarget)
    {
        this.canInteract = canInteract;
        this.interactTarget = interactTarget;
    }

    /// <summary>
    /// ������ �� �Ÿ� �̵��� ���� ������ ���¸� True�� ��ȯ
    /// </summary>
    public void setRollTrue()
    {
        isRoll = true;
    }

    /// <summary>
    /// �����Ⱑ ���� ���¿��� ������ ���¸� False�� ��ȯ
    /// </summary>
    public void setRollFalse()
    {
        isRoll = false;
        animator.SetBool("roll", false);
    }

    /// <summary>
    /// �������� �޾��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(float damage)
    {
        //Debug.Log("Get Damage : " + damage);
        HP -= damage;
        GameManager.Instance.UpdateHPGuage(HP, maxHP);
        if (HP < 0)
        {
            HP = 0;
            Die();
        }
        
    }

    /// <summary>
    /// HP�� 0�� �Ǿ��� �� ȣ��
    /// </summary>
    public void Die()
    {
        // Die Animation
        GameManager.Instance.GameOver();
    }

    public Skill[] AppendSkill(Skill skill)
    {
        GameObject instance = Instantiate(skill.gameObject,transform);
        Skill instanceSkill = instance.GetComponent<Skill>();
        instanceSkill.StartAttack();
        
        haveSkill[currIndex] = instanceSkill;
        skillInstance[currIndex++] = instance;

        return haveSkill;
    }

    /// <summary>
    /// ���� �� �� ȣ�� �Ǵ� �Լ�
    /// </summary>
    public void LevelUp()
    {
        level += 10;
        GameManager.Instance.DrawSkill();
    }

    /// <summary>
    /// ����Ʈ�� ȹ���Ͽ��� �� �������� ����, ����ġ �̻��� �׿����� ������ ȣ��
    /// </summary>
    /// <param name="point"></param>
    public void appendPoint(float point)
    {
         gauge += point;
        if(gauge >= maxGauge)
        {
            LevelUp();
            gauge = gauge - maxGauge;
            maxGauge += gaugeInterval;
        }

        GameManager.Instance.UpdatePointGauge(gauge,maxGauge);
    }

}
