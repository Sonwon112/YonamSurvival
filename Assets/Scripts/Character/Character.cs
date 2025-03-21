using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private Rigidbody2D rbCharacter;
    private Animator animator;

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

        if(horizontal != 0)
        {
            float scaleX = horizontal < 0 ? -1 : 1;
            transform.localScale = new Vector3(scaleX, 1, 1);
        }

        
        rbCharacter.linearVelocity = new Vector2(horizontal*speed, vertical*speed);
        
        animator.SetFloat("speed", Mathf.Abs(rbCharacter.linearVelocityX) + Mathf.Abs(rbCharacter.linearVelocityY));

    }
}
