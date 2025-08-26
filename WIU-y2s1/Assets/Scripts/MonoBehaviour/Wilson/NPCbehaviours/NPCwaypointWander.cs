using UnityEngine;

public class NPCwaypointWander : MonoBehaviour
{
    [SerializeField] private Vector2 waypointStation1 = Vector2.zero;
    [SerializeField] private Vector2 waypointStation2 = Vector2.zero;

    private GameObject player; //player in scene (please ensure your player is tagged "Player").
    private Rigidbody2D rb;
    private Animator animator;

    private int wanderState = 1;
    private float stateBuffer = 2f;

    private float waypointThreshold = 1f;
    private float derenderThreshold = 16f;

    private bool _LEFT = false; //outputs for the NPC.
    private bool _RIGHT = false;

    private Vector2 toWaypoint = Vector2.zero;
    private Vector2 toPlayer = Vector2.zero;

    private float NPCspeed = 0f; //stat for the NPC. Do not initialise anything here, do it under Start().

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wanderState = Random.Range(1, 4);
        NPCspeed = 1f;
    }

    private void FixedUpdate()
    {
        toPlayer = player.transform.position - transform.position;
        if (toPlayer.magnitude <= derenderThreshold && player != null)
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
        }
        else
        {
            _LEFT = false;
            _RIGHT = false;
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
