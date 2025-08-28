using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class bossFSM : MonoBehaviour
{
    private GameObject player; //player in scene (please ensure your player is tagged "Player").
    private GameObject boss;
    private Rigidbody2D rb; //boss Rigidbody2D.

    private Animator animator;

    [SerializeField] private GameObject projectilePrefab;

    private CircleCollider2D attackCollider;

    private Vector2 toPlayer = Vector2.zero;

    private float bossSpeed = 0f;
    private float bossJump = 0f;

    private EntityStatistics stats;  // Added by Jovan Yeo Kaisheng

    // Remove phased2 health -- Jovan Yeo Kaisheng

    //phase 1.1 variables start

    private const float perimeterDistance = 6f;
    private float subStateTransitionTimer = 5f;

    //phase 1.1 variables end
    //phase 1.2 variables start

    private int attackSubStateAmount = 0;
    private enum attackSubStateEnum
    {
        charge,
        attack,
        recuperate
    }
    private attackSubStateEnum attackSubState = attackSubStateEnum.charge;
    private float attackingTimer = 0.25f;

    //phase 1.2 variables end
    //phase 2.1 variables start

    private float bombardPrimeTimer = 0.5f;

    private float projectileVel = 10f;
    private float gravity = 10f;
    private float firePower = 10f;

    private float aim = 0f;

    private int bombardProjectiles = 1;
    private float projectileTimer = 0.25f;

    //phase 2.1 variables end

    private Vector2 movementTarget = Vector2.zero;

    private bool isPerimeterLEFT = false;

    private bool _LEFT = false;
    private bool _RIGHT = false;
    private bool _JUMP = false;

    private enum subStateEnum
    {
        evade1,
        attack1,
        prime2,
        bombard2,
        attack2
    }
    private subStateEnum subState = subStateEnum.evade1;

    private enum phaseStateEnum
    {
        phaseOne,
        phaseTwo,
        phaseThree
    }
    private phaseStateEnum phaseState = phaseStateEnum.phaseOne;

    private bool doNotRefreshStatesFlag = false;

    private void Start()
    {
        // Modified by Jovan Yeo Kaisheng
        bossSpeed = 1f * Time.deltaTime; 
        bossJump = 10f;
        boss = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");


        rb = boss.GetComponent<Rigidbody2D>();
        attackCollider = boss.GetComponent<CircleCollider2D>();
        animator = boss.GetComponent<Animator>();

        stats = boss.GetComponent<EntityStatistics>(); // Added by Jovan Yeo Kaisheng

        
        doNotRefreshStatesFlag = false;

        //phaseState = phaseStateEnum.phaseTwo;
        //subState = subStateEnum.prime2;
        //attackSubState = attackSubStateEnum.charge;
    }
    private void Update()
    {
        //Added by Dylan Yap to ensure boss does not update outside of player's vision
        if (Vector2.Distance(transform.position, player.transform.position) > 40) return;

        //Debug.Log(attackSubStateAmount);

        if (stats != null && stats.maxHealth > 0)
        {
            if (stats != null && stats.GetHealthRatio() <= 0.5f && !doNotRefreshStatesFlag)
            {
                phaseState = phaseStateEnum.phaseTwo;
                subState = subStateEnum.prime2;
                attackSubState = attackSubStateEnum.charge;
                doNotRefreshStatesFlag = true;
            }
        }

        toPlayer = player.transform.position - boss.transform.position;
        if (phaseState == phaseStateEnum.phaseOne)
        {
            if (subState == subStateEnum.evade1)
            {
                if (subStateTransitionTimer <= 0)
                {
                    subState = subStateEnum.attack1;
                    attackSubStateAmount = 5;
                    subStateTransitionTimer = 5f;
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
                    if (toPlayer.magnitude < 3f)
                    {
                        attackSubState = attackSubStateEnum.attack;
                        _JUMP = Random.Range(0, 1) == 0;
                    }
                }
                else if (attackSubState == attackSubStateEnum.attack)
                {
                    attackCollider.enabled = true;
                    animator.SetBool("isAttacking", true);
                    attackingTimer -= Time.deltaTime;
                    if (attackingTimer <= 0)
                    {
                        attackingTimer = 0.25f;
                        isPerimeterLEFT = !isPerimeterLEFT;
                        attackSubState = attackSubStateEnum.recuperate;
                    }
                }
                else if (attackSubState == attackSubStateEnum.recuperate)
                {
                    attackCollider.enabled = false;
                    animator.SetBool("isAttacking", false);
                    movementTarget = new Vector2(player.transform.position.x + (isPerimeterLEFT ? -perimeterDistance * 1f : perimeterDistance * 1f), player.transform.position.y);
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
        }
        else if (phaseState == phaseStateEnum.phaseTwo)
        {
            if (subState == subStateEnum.prime2)
            {
                movementTarget = (Vector2)(transform.position) - toPlayer;
                bombardPrimeTimer -= Time.deltaTime;

                if (bombardPrimeTimer <= 0)
                {
                    bombardPrimeTimer = 0.5f;
                    subState = subStateEnum.bombard2;
                }
            }
            else if (subState == subStateEnum.bombard2)
            {
                if (projectileTimer  <= 0)
                {
                    projectileTimer = 0.25f;
                    bombardProjectiles--;
                    if (bombardProjectiles <= 0)
                    {
                        subState = subStateEnum.attack2;
                    }
                    var projectile = Instantiate(projectilePrefab);
                    projectile.transform.position = transform.position;
                    int validAngles = 0;
                    var discriminant = (Mathf.Pow(projectileVel, 4)) - (gravity * ((gravity * toPlayer.x * toPlayer.x) + (2 * (toPlayer.y * projectileVel * projectileVel))));
                    if (discriminant < 0)
                    {
                        validAngles = 0;
                    }
                    else if (discriminant == 0)
                    {
                        validAngles = 1;
                    }
                    else
                    { validAngles = 2; }
                    float sqrtDisc = Mathf.Sqrt(discriminant);
                    float angle1 = Mathf.Atan((projectileVel * projectileVel + sqrtDisc) / (gravity * toPlayer.x));
                    float angle2 = Mathf.Atan((projectileVel * projectileVel - sqrtDisc) / (gravity * toPlayer.x));
                    RaycastHit2D hitResult = Physics2D.Raycast(transform.position, toPlayer, 3, LayerMask.GetMask("Ground"));
                    float[] validAngleArray = new float[] { angle1, angle2 };
                    aim = validAngleArray.Length == 1 ? validAngleArray[0] : (hitResult.collider != null ? validAngleArray[0] : validAngleArray[1]);
                    Vector2 launchDir = new Vector2(Mathf.Cos(aim), Mathf.Sin(aim));
                    launchDir *= Mathf.Sign(toPlayer.x);
                    if (validAngles == 0)
                    {
                        launchDir = new Vector2(toPlayer.x >= 0f ? .75f : -.75f, .75f);
                    }
                    projectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(launchDir.x * firePower, launchDir.y * firePower);
                    projectile.GetComponent<Rigidbody2D>().angularVelocity = -(projectile.GetComponent<Rigidbody2D>().linearVelocityX) * 100f;
                }
                else
                {
                    projectileTimer -= Time.deltaTime;
                }
            }
            else if (subState == subStateEnum.attack2)
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
                    animator.SetBool("isAttacking", true);
                    attackCollider.enabled = true;
                    attackingTimer -= Time.deltaTime;
                    if (attackingTimer <= 0)
                    {
                        attackingTimer = 0.25f;
                        isPerimeterLEFT = !isPerimeterLEFT;
                        attackSubState = attackSubStateEnum.recuperate;
                    }
                }
                else if (attackSubState == attackSubStateEnum.recuperate)
                {
                    animator.SetBool("isAttacking", false);
                    attackCollider.enabled = false;
                    movementTarget = new Vector2(player.transform.position.x + (isPerimeterLEFT ? -perimeterDistance : perimeterDistance), player.transform.position.y);
                    _JUMP = true;
                    if (toPlayer.magnitude > 6f)
                    {
                        attackSubState = attackSubStateEnum.charge;
                        if (attackSubStateAmount <= 0)
                        {
                            subState = subStateEnum.bombard2;
                            attackSubStateAmount = 2;
                            subStateTransitionTimer = 3f;
                        }
                        else
                        {
                            attackSubStateAmount -= 1;
                        }
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
            //RaycastHit2D playerChecker = Physics2D.Raycast(boss.transform.position, Vector2.down, 2f, LayerMask.GetMask("Default"));
            if (groundChecker.collider != null)
            {
                rb.linearVelocityY = bossJump;
            }
        }

        handleMovementInput();
        rb.linearVelocityX *= 0.99f;
        rb.linearVelocityY -= 10f * Time.deltaTime;

        if (rb.linearVelocity.magnitude > 1f)
        {
            animator.SetBool("isMobile", true);
        }
        else
        {
            animator.SetBool("isMobile", false);
        }

        transform.localScale = toPlayer.x < 0 ? new Vector2(10, 10) : new Vector2(-10, 10);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Hurt(Random.Range(5, 12));
            }
        }
    }
}
