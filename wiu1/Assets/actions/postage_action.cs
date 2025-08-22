using UnityEngine;

[CreateAssetMenu(fileName = "postage_action", menuName = "Scriptable Objects/postage_action")]
public class postage_action : StateAction
{
    public GameObject enemyToTag;
    public GameObject playerToTag;
    public Vector2 origin;
    private bool _LEFT = false;
    private bool _RIGHT = false;
    private bool isGrounded = false;
    private Vector2 toPlayer = Vector2.zero;
    private Rigidbody2D rb;

    private float eneSpeed = 2f;
    private float jumpPower = 10f;
    

    public override void Act(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("ene1");
        var playerInScene = GameObject.FindGameObjectWithTag("player");
        rb = enemyInScene.GetComponent<Rigidbody2D>();
        toPlayer = playerInScene.transform.position - enemyInScene.transform.position;

        Debug.Log("ATTACK");
        _LEFT = !(toPlayer.x > 0);
        _RIGHT = toPlayer.x > 0;

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
}
