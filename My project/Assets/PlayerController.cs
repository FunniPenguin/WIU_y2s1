using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;
    private Vector2 moveDirection;

    public GameObject hurtbox;
    public float speed;
    public float jumpHeight;

    public bool IsGrounded { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetBool("IsGrounded", false);

        var moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += ctx =>
        {
            //Moving action
            moveDirection = ctx.ReadValue<Vector2>();

            if (moveDirection.x < 0)
                transform.localScale = new Vector3(-2, 2, 2);
            else
                transform.localScale = new Vector3(2, 2, 2);

            body.linearVelocityX = moveDirection.x * speed;

            animator.SetBool("IsMoving", true);
        };
        moveAction.canceled += ctx =>
        {
            moveDirection = Vector2.zero;

            body.linearVelocityX = 0;

            animator.SetBool("IsMoving", false);
        };
    }

    void FixedUpdate()
    {
        if (!animator.GetBool("IsAttacking")) 
            hurtbox.SetActive(false);
        if (!animator.GetBool("IsGrounded") && body.linearVelocityY < 0)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }

            if (!animator.GetBool("IsMoving"))
            {
                body.linearVelocityX = 0;
            }

        animator.SetBool("IsGrounded", IsGrounded);

        var attackAction = InputSystem.actions.FindAction("Attack");
        attackAction.started += ctx =>
        {
            animator.SetTrigger("IsAttacking");
            hurtbox.SetActive(true);
        };

        //Jumping action
        var jumpAction = InputSystem.actions.FindAction("Jump");
        jumpAction.performed += ctx =>
        {
            if (animator.GetBool("IsGrounded"))
            {
                body.linearVelocityY = jumpHeight;
                animator.SetBool("IsJumping", true);
            }
        };
        jumpAction.canceled += ctx =>
        {
            body.linearVelocityY *= 0.5f;
            animator.SetBool("IsJumping", false);
        };
    }
}
