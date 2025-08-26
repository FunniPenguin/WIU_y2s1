using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class NPCwander : MonoBehaviour
{
    //sporadic NPC wandering.
    private GameObject player; //player in scene (please ensure your player is tagged "Player").
    private Rigidbody2D rb; //NPC's rigidBody2D.

    private Vector2 toPlayer = Vector2.zero; //vector from the NPC to the player.
    private float stoppingThreshold = 1f; //distance to the player where all movement is halted.

    private int wanderState = 1; //essentially an enum for the NPC's wander behaviour.
    private float stateBuffer = 0f; //timer for the above states.

    private bool _LEFT = false; //outputs for the NPC.
    private bool _RIGHT = false;
    private bool _JUMP = false;
    private bool isGrounded = false;

    private float NPCspeed = 0f; //stats for the NPC. Do not initialise anything here, do it under Start().
    private float NPCjump = 0f;

    private const float shortInterval = 0.5f; //intervals for the above enum.
    private const float mediumInterval = 1.5f;
    private const float longInterval = 3f;
    private const float veryLongInterval = 5.5f;
    private float hypotenuseRaycast = Mathf.Sqrt(5); //exclusively for validateForwardSafety(char).

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        wanderState = Random.Range(1, 10);
        NPCspeed = 1f;
        NPCjump = 6f;
    }

    
    void FixedUpdate()
    {
        if (stateBuffer <= 0f) { wanderState = Random.Range(1, 10); } //upon cooldown, transition to another state.
        stateBuffer = stateBuffer <= 0f ? //checks if the state buffer timer has ended.
            wanderState == 1 || wanderState == 4 || wanderState == 7 ? //if it has, take a look at what new state it is now.
            shortInterval : //refresh the state buffer timer with the appropriate new value.
            wanderState == 2 || wanderState == 5 || wanderState == 8 ? //ditto, for medium duration.
            mediumInterval :
            wanderState != 10 ? //checks if the state is not 10 (which is the only state with a "very long" duration.
            longInterval : 
            veryLongInterval :
            stateBuffer - Time.deltaTime; //otherwise, the timer is still ongoing and a deltaTime decrement can be applied.
        _LEFT = wanderState <= 3; //wanderState states 1, 2, and 3 all involve moving left.
        _RIGHT = wanderState >= 4 && wanderState <= 6; //wanderState states 4, 5, and 6 all involve moving right.

        if (player != null) //in case a game object with the tag "Player" doesn't exist yet, skip this code altogether to prevent the entire script from crashing.
        {
            toPlayer = (Vector2)(player.transform.position - transform.position); //the vector from the NPC to the player.

            if (toPlayer.magnitude <= stoppingThreshold) //if the NPC is too close to the player...
            {
                _LEFT = false; //...stop moving.
                _RIGHT = false;
            }
        }

        bindMovement(_LEFT, _RIGHT); //apply force to the rigidBody2D based on the two movement variables.

        _JUMP = ((_LEFT && inspectPertinentJump('l')) || (_RIGHT && inspectPertinentJump('r'))); //checks if solid geometry is in front of where it's trying to move.
        isGrounded = checkIfGrounded(); //as the names suggest, checks for grounded status.
        if (_JUMP && isGrounded) //if they need to jump and they're on the ground...
        {
            executeJump(); //...execute a jump.
        }

        //DEBUG TEXT AND LINES /////////////////////////////////////////////////////////////////////////////////////DELETE ONCE COMPLETE
        Debug.Log("LEFT: " + _LEFT);
        Debug.Log("RIGHT: " + _RIGHT);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x - 1, transform.position.y - 2));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x + 1, transform.position.y - 2));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x - 1, transform.position.y));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x + 1, transform.position.y));
        //DEBUG TEXT AND LINES /////////////////////////////////////////////////////////////////////////////////////DELETE ONCE COMPLETE

        /*  
         *  
         *  1: short left
         *  2: med left
         *  3: long left
         *  4: short right
         *  5: med right
         *  6: long right
         *  7: short idle
         *  8: med idle
         *  9: long idle
         *  10: very long idle
         * 
         */

        rb.linearVelocityX *= 0.95f; //peters out lateral linear velocity.
        //vv a bit of a failsafe that uses two raycasts to discourage the NPC from throwing themselves off a tall place.
        rb.AddForce(new Vector2(!validateForwardSafety('l') && validateForwardSafety('r') ? NPCspeed * 5 : validateForwardSafety('l') && !validateForwardSafety('r') ? NPCspeed * -5 : 0, 0));
    }

    void bindMovement(bool left, bool right) //inputs: left and right booleans, bestows lateral linear velocity accordingly.
    {
        rb.AddForce(new Vector2(left && !right ? validateForwardSafety('l') ? -NPCspeed : NPCspeed * 2 : !left && right ? validateForwardSafety('r') ? NPCspeed : -NPCspeed * 2 : 0f, 0f));
    }

    void executeJump() //perform a jump, additionally nudge the NPC towards where they're headed to mitigate getting stuck.
    {
        rb.linearVelocityY = NPCjump;
        rb.linearVelocityX += _LEFT ? NPCspeed * -0.5f : NPCspeed * 0.5f;
    }

    bool validateForwardSafety(char dir) //checks below and to the side of the NPC to check if proceeding forward is safe.
    {
        RaycastHit2D leftSafetyValidator = Physics2D.Raycast(transform.position, new Vector2(-1f, -2f), hypotenuseRaycast, LayerMask.GetMask("Ground"));
        RaycastHit2D rightSafetyValidator = Physics2D.Raycast(transform.position, new Vector2(1f, -2f), hypotenuseRaycast, LayerMask.GetMask("Ground"));
        return dir == 'l' ? leftSafetyValidator.collider != null : dir == 'r' ? rightSafetyValidator.collider != null : false;
    }

    bool inspectPertinentJump(char dir) //checks directly adjacent to the NPC to check if they're getting blocked by world geometry.
    {
        RaycastHit2D leftJumpValidator = Physics2D.Raycast(transform.position, new Vector2(-1f, 0f), 1f, LayerMask.GetMask("Ground"));
        RaycastHit2D rightJumpValidator = Physics2D.Raycast(transform.position, new Vector2(1f, 0f), 1f, LayerMask.GetMask("Ground"));
        return dir == 'l' ? leftJumpValidator.collider != null : dir == 'r' ? rightJumpValidator.collider != null : false;
    }

    bool checkIfGrounded() //checks directly under the NPC to check if they're contacting the ground.
    {
        RaycastHit2D groundChecker = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        return groundChecker.collider != null;
    }
}
