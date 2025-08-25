using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "attack_action", menuName = "Scriptable Objects/attack_action")]
public class attack_action : StateAction
{
    //public GameObject enemyToTag;
    //public GameObject playerToTag; 
    private bool _LEFT = false;
    private bool _RIGHT = false;
    private bool isGrounded = false;
    private Vector2 toPlayer = Vector2.zero;
    private Rigidbody2D rb;

    private float eneSpeed = 2f;
    private float jumpPower = 10f;

    private Animator animator;

    public override void Act(StateController controller)
    {
        eneSpeed = 2f;
        var enemyInScene = GameObject.FindGameObjectWithTag("Enemy1");
        var playerInScene = GameObject.FindGameObjectWithTag("Player");
        rb = enemyInScene.GetComponent<Rigidbody2D>();
        animator = enemyInScene.GetComponent<Animator>();
        toPlayer = playerInScene.transform.position - enemyInScene.transform.position;
        enemyInScene.transform.localScale = new Vector3(Mathf.Sign(toPlayer.x) * 2, 2, 2);
        Debug.Log("ATTACK");
        _LEFT = !(toPlayer.x > 0);
        _RIGHT = toPlayer.x > 0;

        Debug.Log(toPlayer.magnitude);
        if (toPlayer.magnitude < 2.1f)
        {
            animator.SetTrigger("isAttacking");
        }

        handleMovement();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.95f, rb.linearVelocity.y * 1, 0);
    }

    private void handleMovement()
    {
        float moveDir = 0;

        if (_LEFT) moveDir = 1;
        if (_RIGHT) moveDir = -1;

        rb.linearVelocity = new Vector2(moveDir * eneSpeed, rb.linearVelocity.y);

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
