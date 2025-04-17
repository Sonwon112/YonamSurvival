using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Character : MonoBehaviour, TakeDamage
{
    [Header("State Variable")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float runSpeed = 5.5f;
    [SerializeField] private float diagonalTmp = 0.7f;
    [SerializeField] private float rollPow = 1f;

    [Header("Speed buff Variable")]
    private float buffSpeed = 3.0f;
    private float buffRunSpeed = 5.5f;
    private Coroutine speedBoostCoroutine;


   

    private Rigidbody2D rbCharacter;
    private Animator animator;


    // �÷��̾����
    private float direction = 1;
    private bool isRoll = false;
    public float maxHP = 100f;
    public float HP; 
    
    
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
        HP = maxHP;

        // �ӵ� ������ �⺻ �ӵ� ����
        buffSpeed = speed;
        buffRunSpeed = runSpeed;
        


    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float isRun = Input.GetAxis("Run");
        float tmpSpeed = isRun > 0 ? runSpeed : speed; // �޸������ΰ�? �޸��� runSpeed �ƴϸ� speed

        animator.SetFloat("RunSpeed", tmpSpeed);

        if (horizontal != 0)
        {
            direction = horizontal < 0 ? -1 : 1;
            transform.localScale = new Vector3(direction, 1, 1);
        }

        float absHor = Mathf.Abs(horizontal);
        float absVert = Mathf.Abs(vertical);

        float movingSpeed = absHor + absVert > 1.1 ? tmpSpeed * diagonalTmp : tmpSpeed;
        float vx = horizontal * movingSpeed;
        float currRollPow = isRoll ? direction * rollPow : 0f;

        //Debug.Log(isRoll);

        rbCharacter.linearVelocity = new Vector2(horizontal * movingSpeed + currRollPow, vertical * movingSpeed);

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
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
            die();
        }
        
    }

    /// <summary>
    /// HP�� 0�� �Ǿ��� �� ȣ��
    /// </summary>
    public void die()
    {

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
    /// ü�� �������� �Ծ����� �Լ�
    /// </summary>
    /// <param name="healAmount">ȸ���� ü�¾�</param>
    public void upHeal(float healAmount) {
        HP = Mathf.Min(healAmount + HP , maxHP);
        Debug.Log($"���� ü��: {HP}/{maxHP}");
    }




    /// <summary>
    /// �ӵ� ���� �������� �Ծ����� �Լ�
    /// </summary>
    /// <param name="upSpeed">������ų �ӵ�</param>
    /// <param name="duration">�ӵ� ������ ���ӽð�</param>
    public void ApplySpeedBoost(float upSpeed, float duration)
    {
        // �̹� �ڷ�ƾ ���� �ִٸ� �����ϰ� ���� ����
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }
        speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(upSpeed, duration));
    }


    /// <summary>
    /// �ӵ� ���� �������� �Ծ����� �ڷ�ƾ
    /// </summary>
    /// <param name="upSpeed">������ų �ӵ�</param>
    /// <param name="duration">�ӵ� ������ ���ӽð�</param>
    private System.Collections.IEnumerator SpeedBoostCoroutine(float upSpeed , float duration)
    {
        /// ���� �ӵ��� ���� �ӵ��� ���Ѵ�
        speed *= upSpeed;
        runSpeed *= upSpeed;

        Debug.Log($"�ӵ� ����! �̵�: {speed}, �޸���: {runSpeed}");

        // duration ��ŭ ����
        yield return new WaitForSeconds(duration);

        // ���� �ӵ��� ����
        speed = buffSpeed;
        runSpeed = buffRunSpeed;

        Debug.Log("�ӵ� ���� ȿ�� ����");
    }

}
