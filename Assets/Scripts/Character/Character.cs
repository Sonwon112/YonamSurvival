using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("State Variable")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float runSpeed = 5.5f;
    [SerializeField] private float diagonalTmp = 0.7f;
    [SerializeField] private float rollPow = 1f;

    private Rigidbody2D rbCharacter;
    private Animator animator;

    
    private float direction = 1;
    private bool isRoll = false;


    // 상호작용
    private bool canInteract;
    private Interactable interactTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbCharacter = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float isRun = Input.GetAxis("Run");
        float tmpSpeed = isRun > 0 ? runSpeed : speed;

        animator.SetFloat("RunSpeed",tmpSpeed);

        if(horizontal != 0)
        {
            direction= horizontal < 0 ? -1 : 1;
            transform.localScale = new Vector3(direction, 1, 1);
        }

        float absHor = Mathf.Abs(horizontal);
        float absVert = Mathf.Abs(vertical);

        float movingSpeed = absHor + absVert > 1.1 ? tmpSpeed * diagonalTmp : tmpSpeed;
        float vx = horizontal * movingSpeed;
        float currRollPow = isRoll ? direction * rollPow : 0f;

        //Debug.Log(isRoll);

        rbCharacter.linearVelocity = new Vector2(horizontal* movingSpeed + currRollPow, vertical* movingSpeed);
        
        animator.SetFloat("speed", absHor + absVert);

        if (Input.GetButtonDown("Jump") && !isRoll)
        {
            //isRoll = true;
            animator.SetBool("roll", true);
            Invoke("setRollTrue", 0.1f);
        }

        if (canInteract)
        {
            if (Input.GetButtonDown("MoveUpper"))
            {
                interactTarget.Interact(gameObject, "up");
            }else if (Input.GetButtonDown("MoveUnder"))
            {
                interactTarget.Interact(gameObject, "down");
            }
        }

    }

    public void setCanInteract(bool canInteract, Interactable interactTarget)
    {
        this.canInteract = canInteract;
        this.interactTarget = interactTarget;
    }

    public void setRollTrue()
    {
        isRoll = true;
    }

    public void setRollFalse()
    {
        isRoll = false;
        animator.SetBool("roll", false);
    }

}
