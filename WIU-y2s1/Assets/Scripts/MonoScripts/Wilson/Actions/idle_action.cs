using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "idle_action", menuName = "Scriptable Objects/idle_action")]
public class idle_action : StateAction
{
    //public GameObject enemyToTag;
    //private bool isFacingLeft = true;
    private Vector2 toPlayer = Vector2.zero;
    public Rigidbody2D rb;
    private Animator animator;
    private float eneSpeed = 2f;

    private float idleTimer = 0.25f;
    private int idleState = 1;

    private bool _LEFT = false;
    private bool _RIGHT = false;

   
    public override void Act(StateController controller)
    {
        
        var enemyInScene = GameObject.FindGameObjectWithTag("Enemy1");
        var playerInScene = GameObject.FindGameObjectWithTag("Player");
        rb = enemyInScene.GetComponent<Rigidbody2D>();
        animator = enemyInScene.GetComponent<Animator>();
        switch (idleState)
        {
        case 1:
                _LEFT = true;
                _RIGHT = false;
                //isFacingLeft = true;
                animator.SetBool("isMobile", true);
                break;
        case 2:
                _LEFT = false;
                _RIGHT = false;
                animator.SetBool("isMobile", false);
                break;
        case 3:
                _LEFT = false;
                _RIGHT = true;
                //isFacingLeft = false;
                animator.SetBool("isMobile", true);
                break;
        case 4:
                _LEFT = false;
                _RIGHT = false;
                animator.SetBool("isMobile", false);
                break;
        case 5:
                idleState = 1;
                break;
        default:
            break;
        }
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            float randomDenumerable = Random.Range(25, 150) / 100;
            idleTimer = randomDenumerable;
            idleState++;
        }
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.98f, rb.linearVelocity.y * 1, 0);
        var moveDirection = Mathf.Sign(rb.linearVelocityX + ((playerInScene.transform.position.x - enemyInScene.transform.position.x) > 0 ? 0.1f : -0.1f));
        enemyInScene.transform.localScale = new Vector3(moveDirection * 2 * (_LEFT || _RIGHT ? -1 : 1), 2, 2);
        handleMovement();
    }

    private void handleMovement()
    {
        //Debug.Log("IDLE");
        if (!(_LEFT && _RIGHT))
        {
            if (_LEFT)
            {
                rb.linearVelocity = new Vector3(-eneSpeed, rb.linearVelocity.y, 0);
            }
            if (_RIGHT)
            {
                rb.linearVelocity = new Vector3(eneSpeed, rb.linearVelocity.y, 0);
            }
        }
    }
}
