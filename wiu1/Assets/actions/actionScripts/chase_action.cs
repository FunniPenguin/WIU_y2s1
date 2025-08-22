using UnityEngine;

[CreateAssetMenu(fileName = "chase_action", menuName = "Scriptable Objects/chase_action")]
public class chase_action : StateAction
{
    public GameObject enemyToTag;
    public GameObject playerToTag;
    private bool _LEFT = false;
    private bool _RIGHT = false;
    private bool isFacingLeft = true;
    private Vector2 toPlayer = Vector2.zero;
    private Rigidbody2D rb;

    private float eneCSpeed = 2f;

    public override void Act(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("ene1");
        var playerInScene = GameObject.FindGameObjectWithTag("player");
        rb = enemyInScene.GetComponent<Rigidbody2D>();
        toPlayer = playerInScene.transform.position - enemyInScene.transform.position;

        
        _RIGHT = !(toPlayer.x > 0);
        _LEFT = toPlayer.x > 0;

        isFacingLeft = !(toPlayer.x > 0);
        handleMovement();
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.95f, rb.linearVelocity.y * 1, 0);
    }

    private void handleMovement()
    {
        float moveDir = 0;

        eneCSpeed = 2;
        if (_LEFT) moveDir = 1;
        if (_RIGHT) moveDir = -1;
        Debug.Log("CHASE");
        rb.linearVelocity = new Vector2(moveDir * eneCSpeed, rb.linearVelocity.y);
        Debug.Log(eneCSpeed);
    }
}
