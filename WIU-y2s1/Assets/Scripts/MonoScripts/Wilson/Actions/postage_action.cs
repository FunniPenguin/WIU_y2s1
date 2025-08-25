using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "postage_action", menuName = "Scriptable Objects/postage_action")]
public class postage_action : StateAction
{
    public GameObject enemyToTag;
    public GameObject playerToTag;
    public Vector2 origin;
    private bool _LEFT = false;
    private bool _RIGHT = false;
    private bool _JUMP = false;
    private bool isGrounded = false;
    private Vector2 toPlayer = Vector2.zero;
    private Rigidbody2D rb;

    private float eneSpeed = 4f;
    private float jumpPower = 10f;
    private float jumpThreshold = 5f;
    

    public override void Act(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("ene1");
        var playerInScene = GameObject.FindGameObjectWithTag("player");
        rb = enemyInScene.GetComponent<Rigidbody2D>();
        toPlayer = playerInScene.transform.position - enemyInScene.transform.position;

        Debug.Log("POSTAGE");
        _RIGHT = !(toPlayer.x > 0);
        _LEFT = toPlayer.x > 0;

        if (toPlayer.y > jumpThreshold)
        {
            _JUMP = true;
            Debug.Log("JUMP CALLED");
        }

        handleMovement();

        rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.95f, rb.linearVelocity.y * 1, 0);
    }

    private void handleMovement()
    {
        float moveDir = 0;

        RaycastHit2D hitResult = Physics2D.Raycast(enemyToTag.transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
        isGrounded = hitResult.collider != null;

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
