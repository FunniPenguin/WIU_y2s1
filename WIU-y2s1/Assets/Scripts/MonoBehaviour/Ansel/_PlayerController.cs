using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class _PlayerController : MonoBehaviour, IDataPersistence
{
    private EntityStatistics _statistics;
    private Animator animator;
    private Rigidbody2D body;
    private Vector2 moveDirection;

    private float speed;
    private float jumpHeight = 10;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float MaxfallSpeed = 20f;
    public float fallSpeedMultiplier = 2f;

    public bool IsGrounded { get; set; }
    public bool IsClimbing { get; set; }
    public float _lastSavedDirection = 0;

    void Awake()
    {
        DontDestroyOnLoad(this);
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        _statistics = GetComponent<EntityStatistics>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = _statistics.speed;
        jumpHeight = _statistics.jumpPower;

        animator.SetBool("IsGrounded", false);

        var moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += ctx =>
        {
            //Climbing action
            if (IsClimbing) 
            {

            }

            //Moving action
            moveDirection = ctx.ReadValue<Vector2>();
            _lastSavedDirection = moveDirection.x;

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

    private void Update()
    {
        Gravity();
    }

    void FixedUpdate()
    {
        if (GetComponent<_HealthSystem>().health.health <= 0) 
        {
            _statistics.uponDeath.Invoke();
        }

        if (!animator.GetBool("IsGrounded") && body.linearVelocityY < 0)
        {
            animator.SetBool("IsFalling", true);
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
        };

        //Jumping action
        var jumpAction = InputSystem.actions.FindAction("Jump");
        jumpAction.performed += ctx =>
        {
            if (animator.GetBool("IsGrounded"))
            {
                body.linearVelocityY = jumpHeight;
                animator.SetTrigger("IsJumping");
            }
        };
        jumpAction.canceled += ctx =>
        {
            body.linearVelocityY *= 0.5f;
            animator.SetBool("IsJumping", false);
        };

        //Interact
        var interactAction = InputSystem.actions.FindAction("Interact");
        interactAction.started += ctx =>
        {
            //Put codes here
        };
    }

    private void Gravity()
    {
        if (body.linearVelocity.y < 0)
        {
            body.gravityScale = baseGravity * fallSpeedMultiplier;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(body.linearVelocity.y, -MaxfallSpeed));
        }
        else
        {
            body.gravityScale = baseGravity;
        }
    }
    public void SaveData(ref GameData data)
    {
        data._playerPosition = transform.position;
    }
    public void LoadData(GameData data)
    {
        transform.position = data._playerPosition;
<<<<<<< Updated upstream
    }
=======

    }

        //OnMove function
        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                //Moving action
                moveDirection = ctx.ReadValue<Vector2>();
                _lastSavedDirection = moveDirection.x;

                if (moveDirection.x < 0)
                    transform.localScale = new Vector3(-2, 2, 2);
                else
                    transform.localScale = new Vector3(2, 2, 2);

                body.linearVelocityX = moveDirection.x * speed;

                animator.SetBool("IsMoving", true);
            }
            else if (ctx.canceled)
            {
                moveDirection = Vector2.zero;

                body.linearVelocityX = 0;

                animator.SetBool("IsMoving", false);
            }
        }

        public void OnAttack(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                animator.SetTrigger("IsAttacking");
            }
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (animator.GetBool("IsGrounded"))
                {
                    body.linearVelocityY = jumpHeight;
                    animator.SetTrigger("IsJumping");
                }
            }
            else if (ctx.canceled)
            {
                body.linearVelocityY *= 0.5f;
                animator.SetBool("IsJumping", false);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            if (groundCheckPosition) Gizmos.DrawWireCube(groundCheckPosition.position, groundCheckSize);
        }
>>>>>>> Stashed changes
}
