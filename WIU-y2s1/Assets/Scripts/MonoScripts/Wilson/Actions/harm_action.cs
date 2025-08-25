using UnityEngine;

[CreateAssetMenu(fileName = "harm_action", menuName = "Scriptable Objects/harm_action")]
public class harm_action : StateAction
{
    private bool _LEFT = false;
    private bool _RIGHT = false;
    private bool _JUMP = false;
    private bool isGrounded = false;
    private Vector2 toPlayer = Vector2.zero;
    private Rigidbody2D rb;

    private float eneSpeed = 4f;
    private float jumpPower = 4f;
    private float jumpThreshold = 0.5f;

    private Animator animator;
    public override void Act(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("Enemy3");
        var playerInScene = GameObject.FindGameObjectWithTag("Player");
        rb = enemyInScene.GetComponent<Rigidbody2D>();
        animator = enemyInScene.GetComponent<Animator>();
        Debug.Log("HARM");
        toPlayer = playerInScene.transform.position - enemyInScene.transform.position;

        if (toPlayer.magnitude >= 5f)
        {
            _RIGHT = !(toPlayer.x > 0);
            _LEFT = toPlayer.x > 0;
        }

        if (toPlayer.magnitude <= 2.5f)
        {
            _LEFT = !(toPlayer.x > 0);
            _RIGHT = toPlayer.x > 0;
            animator.SetTrigger("isAttacking");
        }

        handleMovement();

        var moveDirection = Mathf.Sign(rb.linearVelocityX + ((playerInScene.transform.position.x - enemyInScene.transform.position.x) > 0 ? 0.1f : -0.1f));
        enemyInScene.transform.localScale = new Vector3(moveDirection * -4 * (_LEFT || _RIGHT ? -1 : 1), 4, 4);
    }

    private void handleMovement()
    {
        float moveDir = 0;

        if (_LEFT) moveDir = 1;
        if (_RIGHT) moveDir = -1;

        rb.linearVelocity = new Vector2(moveDir * eneSpeed, rb.linearVelocity.y);

        if (isGrounded && _JUMP)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
        }
    }
}
