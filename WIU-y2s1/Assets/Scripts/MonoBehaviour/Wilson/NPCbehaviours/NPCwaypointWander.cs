using UnityEngine;

public class NPCwaypointWander : MonoBehaviour
{
    [SerializeField] private Vector2 waypointStation1 = Vector2.zero;
    [SerializeField] private Vector2 waypointStation2 = Vector2.zero;

    private Rigidbody2D rb;
    private Animator animator;

    private int wanderState = 1;
    private float stateBuffer = 2f;

    private float waypointThreshold = 1f;

    private bool _LEFT = false; //outputs for the NPC.
    private bool _RIGHT = false;

    private Vector2 toWaypoint = Vector2.zero;

    private float NPCspeed = 0f; //stat for the NPC. Do not initialise anything here, do it under Start().

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wanderState = Random.Range(1, 4);
        NPCspeed = 1f;
    }

    private void FixedUpdate()
    {
        Debug.Log(stateBuffer);
        if (wanderState == 2 || wanderState >= 4)
        {
            _LEFT = false; _RIGHT = false;
            stateBuffer -= Time.deltaTime;
            if (stateBuffer <= 0)
            {
                stateBuffer = Random.Range(10f, 25f) / 10f;
                wanderState += 1;
                if (wanderState > 4)
                {
                    wanderState = 1;
                }
            }
        }
        else
        {
            _LEFT = wanderState == 3;
            _RIGHT = wanderState == 1;

            toWaypoint = wanderState == 1 ? (waypointStation1 - (Vector2)(transform.position)) : (waypointStation2 - (Vector2)(transform.position));

            if (toWaypoint.magnitude <= waypointThreshold)
            {
                wanderState += 1;
                if (wanderState > 4)
                {
                    wanderState = 1;
                }
            }
        }

        handleLateralMovement();
    }

    void handleLateralMovement()
    {
        if (_LEFT && !_RIGHT)
        {
            rb.linearVelocityX = -NPCspeed;
            animator.SetBool("isMobile", true);
            transform.localScale = new Vector2(5, 5);
        }
        else if (!_LEFT && _RIGHT)
        {
            rb.linearVelocityX = NPCspeed;
            animator.SetBool("isMobile", true);
            transform.localScale = new Vector2(-5, 5);
        }
        else
        {
            rb.linearVelocityX = 0;
            animator.SetBool("isMobile", false);
            transform.localScale = new Vector2(5, 5);
        }
    }
}
