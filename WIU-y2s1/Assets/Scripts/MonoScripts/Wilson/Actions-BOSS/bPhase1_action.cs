using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "bPhase1_action", menuName = "Scriptable Objects/bPhase1_action")]
public class bPhase1_action : StateAction
{
    private GameObject player; //player in scene (please ensure your player is tagged "Player").
    private Rigidbody2D rb; //boss Rigidbody2D.

    private CircleCollider2D attackCollider;

    private Vector2 toPlayer = Vector2.zero;

    private float bossSpeed = 0f; //do not initialise anything here, do it under Act().
    private float bossJump = 0f;

    //phase 1 variables start

    private const float perimeterDistance = 6f;
    private float subStateTransitionTimer = 3f;

    //phase 1 variables end
    //phase 2 variables start

    private int attackSubStateAmount = 0;
    private enum attackSubStateEnum
    {
        charge,
        attack,
        recuperate
    }
    private attackSubStateEnum attackSubState = attackSubStateEnum.charge;
    private float attackingTimer = 0.25f;

    //phase 2 variables end

    private Vector2 movementTarget = Vector2.zero;

    private bool isPerimeterLEFT = false;

    private bool _LEFT = false;
    private bool _RIGHT = false;
    private bool _JUMP = false;

    private enum subStateEnum
    {
        evade1,
        attack1
    }
    private subStateEnum subState = subStateEnum.evade1;

    public override void Act(StateController controller)
    {
        //Debug.Log(attackSubStateAmount);
        bossSpeed = 30f * Time.deltaTime;
        bossJump = 10f;
        var boss = GameObject.FindGameObjectWithTag("Boss");
        var player = GameObject.FindGameObjectWithTag("Player");
        rb = boss.GetComponent<Rigidbody2D>();
        attackCollider = boss.GetComponent<CircleCollider2D>();

        toPlayer = player.transform.position - boss.transform.position;
        
        if (subState == subStateEnum.evade1)
        {
            if (subStateTransitionTimer <= 0)
            {
                subState = subStateEnum.attack1;
                attackSubStateAmount = 5;
                subStateTransitionTimer = 3f;
            }

            movementTarget = new Vector2(player.transform.position.x + (isPerimeterLEFT ? -perimeterDistance : perimeterDistance), player.transform.position.y);
            RaycastHit2D rightFlipValidator = Physics2D.Raycast(boss.transform.position, new Vector2(3f, -1f), 3.1f, LayerMask.GetMask("Ground"));
            RaycastHit2D leftFlipValidator = Physics2D.Raycast(boss.transform.position, new Vector2(-3f, -1f), 3.1f, LayerMask.GetMask("Ground"));
            if (rightFlipValidator.collider != null && !isPerimeterLEFT)
            {
                isPerimeterLEFT = true;
            }
            if (leftFlipValidator.collider != null && isPerimeterLEFT)
            {
                isPerimeterLEFT = false;
            }

            if (toPlayer.magnitude < 2.5f)
            {
                _JUMP = true;
            }

            subStateTransitionTimer -= Time.deltaTime;


            //Debug.DrawLine(boss.transform.position, new Vector2(boss.transform.position.x + 3, boss.transform.position.y - 1));
            //Debug.DrawLine(boss.transform.position, new Vector2(boss.transform.position.x - 3, boss.transform.position.y - 1));
        }
        else if (subState == subStateEnum.attack1)
        {
            if (attackSubState == attackSubStateEnum.charge)
            {
                movementTarget = player.transform.position;
                if (toPlayer.magnitude < 2.5f)
                {
                    attackSubState = attackSubStateEnum.attack;
                    _JUMP = Random.Range(0, 1) == 0;
                }
            }
            else if (attackSubState == attackSubStateEnum.attack)
            {
                attackCollider.enabled = true;
                attackingTimer -= Time.deltaTime;
                if (attackingTimer <= 0)
                {
                    attackingTimer = 0.25f;
                    isPerimeterLEFT = Random.Range(0, 1) == 0;
                    attackSubState = attackSubStateEnum.recuperate;
                }
            }
            else if (attackSubState == attackSubStateEnum.recuperate)
            {
                attackCollider.enabled = false;
                movementTarget = new Vector2(player.transform.position.x + (isPerimeterLEFT ? -perimeterDistance : perimeterDistance), player.transform.position.y);
                _JUMP = true;
                if (toPlayer.magnitude > 6f)
                {
                    attackSubState = attackSubStateEnum.charge;
                    if (attackSubStateAmount <= 0)
                    {
                        subState = subStateEnum.evade1;
                        attackSubStateAmount = 5;
                        subStateTransitionTimer = 3f;
                    }
                    else
                    {
                        attackSubStateAmount -= 1;
                    }
                }
            }
        }

        _LEFT = movementTarget.x < boss.transform.position.x;
        _RIGHT = movementTarget.x > boss.transform.position.x;

        if (_JUMP)
        {
            _JUMP = false;
            RaycastHit2D groundChecker = Physics2D.Raycast(boss.transform.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));
            if (groundChecker.collider != null)
            {
                rb.linearVelocityY = bossJump;
            }
        }

        handleMovementInput();
        rb.linearVelocityX *= 0.99f;
        rb.linearVelocityY -= 10f * Time.deltaTime;
    }

    private void handleMovementInput()
    {
        if (_LEFT && !_RIGHT)
        {
            rb.linearVelocityX -= bossSpeed;
        }
        else if (!_LEFT && _RIGHT)
        {
            rb.linearVelocityX += bossSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision == GameObject.FindGameObjectWithTag("Player"))
    }
}
