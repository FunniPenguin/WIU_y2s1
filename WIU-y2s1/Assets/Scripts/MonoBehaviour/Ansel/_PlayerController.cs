using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class _PlayerController : MonoBehaviour
{
    private EntityStatistics _statistics;
    private Animator animator;
    private Rigidbody2D body;
    private Vector2 moveDirection;

    public float speed;
    public float jumpHeight = 10;

    [Header("Ground Check")]
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float MaxfallSpeed = 20f;
    public float fallSpeedMultiplier = 2f;

    // Added by Jovan Yeo Kaisheng
    [Header("Events")]
    public UnityEvent onMoveStart;
    public UnityEvent onMoveEnd;
    public UnityEvent onJump;
    public UnityEvent onLand;

    public bool IsGrounded { get; set; }
    public bool IsDead { get; set; }
    public bool IsClimbing { get; set; }
    public float _lastSavedDirection = 0;

    private bool wasGrounded;

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        _statistics = GetComponent<EntityStatistics>();
    }
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPosition.position, groundCheckSize, 0, groundLayer))
        {
            IsGrounded = true;
            if (!IsGrounded)
            {
                onLand?.Invoke();
            }
        }
        else
        {
            IsGrounded = false;
        }

        wasGrounded = IsGrounded;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = _statistics.speed;
        jumpHeight = _statistics.jumpPower;

        animator.SetBool("IsGrounded", false);
        animator.SetBool("IsDead", false);

        var moveAction = InputSystem.actions.FindAction("Move");
        DataPersistenceManager.Instance.Load();
    }

    private void Update()
    {
        Gravity();
        GroundCheck();
    }

    void FixedUpdate()
    {
        //if (GetComponent<HealthSystem>(). <= 0) 
        //{
        //    _statistics.uponDeath.Invoke();
        //}

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
        animator.SetBool("IsDead", IsDead);
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
            onMoveStart?.Invoke();
        }
        else if (ctx.canceled)
        {
            moveDirection = Vector2.zero;

            body.linearVelocityX = 0;

            animator.SetBool("IsMoving", false);
            onMoveEnd?.Invoke();
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
                onJump?.Invoke();
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
}
