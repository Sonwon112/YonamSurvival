using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("State Variable")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float runSpeed = 5.5f;
    [SerializeField] private float diagonalTmp = 0.7f;

    private Rigidbody2D rbCharacter;
    private Animator animator;

    public Vector2 facingDirection = Vector2.right;

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
            float scaleX = horizontal < 0 ? -1 : 1;
            transform.localScale = new Vector3(scaleX, 1, 1);
        }

        float absHor = Mathf.Abs(horizontal);
        float absVert = Mathf.Abs(vertical);

        float movingSpeed = absHor + absVert > 1.1 ? tmpSpeed * diagonalTmp : tmpSpeed;

        rbCharacter.linearVelocity = new Vector2(horizontal* movingSpeed, vertical* movingSpeed);
        
        animator.SetFloat("speed", absHor + absVert);

    }
}
