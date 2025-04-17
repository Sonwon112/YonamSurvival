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


    // 플레이어상태
    private float direction = 1;
    private bool isRoll = false;
    public float maxHP = 100f;
    public float HP; 
    
    
    // 스킬
    private Skill[] haveSkill = new Skill[5];
    private GameObject[] skillInstance = new GameObject[5];
    private int currIndex = 0;

    // 상호작용
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

        // 속도 버프의 기본 속도 저장
        buffSpeed = speed;
        buffRunSpeed = runSpeed;
        


    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float isRun = Input.GetAxis("Run");
        float tmpSpeed = isRun > 0 ? runSpeed : speed; // 달리는중인가? 달리면 runSpeed 아니면 speed

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
        // 구르기
        if (Input.GetButtonDown("Jump") && !isRoll)
        {
            //isRoll = true;
            animator.SetBool("roll", true);
            Invoke("setRollTrue", 0.1f);
        }
        // 상호작용
        if (canInteract)
        {
            // 층수 이동
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
    /// 상호작용 물체 접근 / 탈출 시 상호작용 상태 변경
    /// </summary>
    /// <param name="canInteract"></param>
    /// <param name="interactTarget"></param>
    public void setCanInteract(bool canInteract, Interactable interactTarget)
    {
        this.canInteract = canInteract;
        this.interactTarget = interactTarget;
    }

    /// <summary>
    /// 구르기 시 거리 이동을 위한 구르기 상태를 True로 변환
    /// </summary>
    public void setRollTrue()
    {
        isRoll = true;
    }

    /// <summary>
    /// 구르기가 끝난 상태에서 구르기 상태를 False로 변환
    /// </summary>
    public void setRollFalse()
    {
        isRoll = false;
        animator.SetBool("roll", false);
    }

    /// <summary>
    /// 데미지를 받았을 때 호출되는 함수
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
    /// HP가 0이 되었을 때 호출
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
    /// 체력 아이템을 먹었을때 함수
    /// </summary>
    /// <param name="healAmount">회복할 체력양</param>
    public void upHeal(float healAmount) {
        HP = Mathf.Min(healAmount + HP , maxHP);
        Debug.Log($"현재 체력: {HP}/{maxHP}");
    }




    /// <summary>
    /// 속도 버프 아이템을 먹었을때 함수
    /// </summary>
    /// <param name="upSpeed">증가시킬 속도</param>
    /// <param name="duration">속도 버프의 지속시간</param>
    public void ApplySpeedBoost(float upSpeed, float duration)
    {
        // 이미 코루틴 돌고 있다면 중지하고 새로 시작
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }
        speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(upSpeed, duration));
    }


    /// <summary>
    /// 속도 버프 아이템을 먹었을때 코루틴
    /// </summary>
    /// <param name="upSpeed">증가시킬 속도</param>
    /// <param name="duration">속도 버프의 지속시간</param>
    private System.Collections.IEnumerator SpeedBoostCoroutine(float upSpeed , float duration)
    {
        /// 현재 속도에 증가 속도를 곱한다
        speed *= upSpeed;
        runSpeed *= upSpeed;

        Debug.Log($"속도 증가! 이동: {speed}, 달리기: {runSpeed}");

        // duration 만큼 지속
        yield return new WaitForSeconds(duration);

        // 원래 속도로 복원
        speed = buffSpeed;
        runSpeed = buffRunSpeed;

        Debug.Log("속도 증가 효과 종료");
    }

}
