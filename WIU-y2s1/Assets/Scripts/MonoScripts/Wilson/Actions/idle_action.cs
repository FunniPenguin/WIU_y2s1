using UnityEngine;

[CreateAssetMenu(fileName = "idle_action", menuName = "Scriptable Objects/idle_action")]
public class idle_action : StateAction
{
    //public GameObject enemyToTag;
    private bool isFacingLeft = true;
    private Vector2 toPlayer = Vector2.zero;
    public Rigidbody2D rb;
    private float eneSpeed = 2f;

    private float idleTimer = 0.25f;
    private int idleState = 1;

    private bool _LEFT = false;
    private bool _RIGHT = false;

   
    public override void Act(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("ene1");
        rb = enemyInScene.GetComponent<Rigidbody2D>();
        switch (idleState)
        {
        case 1:
            _LEFT = true;
            _RIGHT = false;
            isFacingLeft = true;
            break;
        case 2:
            _LEFT = false;
            _RIGHT = false;
            break;
        case 3:
            _LEFT = false;
            _RIGHT = true;
            isFacingLeft = false;
            break;
        case 4:
            _LEFT = false;
            _RIGHT = false;
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
        handleMovement();
    }

    private void handleMovement()
    {
        Debug.Log("IDLE");
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
