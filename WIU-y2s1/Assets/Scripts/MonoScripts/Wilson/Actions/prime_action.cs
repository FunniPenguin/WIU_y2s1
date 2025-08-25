using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "prime_action", menuName = "Scriptable Objects/prime_action")]
public class prime_action : StateAction
{
    //public GameObject enemyToTag;
    //public GameObject playerToTag;
    
    private Rigidbody2D rb;
    private Animator animator;

    public Vector2 turretAimDirection = Vector2.zero;

    private float projectileVel = 10f;
    private float gravity = 10f;
    [SerializeField] private GameObject _bulletPrefab;
    private float firePower = 10f;

    private Vector2 dispVec = Vector2.zero;
    private float offensiveAim = 0f;
    private float discriminant = 0;
    private int validAngles = 0;

    

    private bool _SHOOT = false;

    private float shootCooldown = 1f;


    public override void Act(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("Enemy2");
        var playerInScene = GameObject.FindGameObjectWithTag("Player");
        rb = enemyInScene.GetComponent<Rigidbody2D>();
        animator = enemyInScene.GetComponent<Animator>();
        animator.SetBool("isAttacking", true);

        dispVec = (Vector2)(playerInScene.transform.position - enemyInScene.transform.position);
        discriminant = (Mathf.Pow(projectileVel, 4)) - (gravity * ((gravity * dispVec.x * dispVec.x) + (2 * (dispVec.y * projectileVel * projectileVel))));
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
        float angle1 = Mathf.Atan((projectileVel * projectileVel + sqrtDisc) / (gravity * dispVec.x));
        float angle2 = Mathf.Atan((projectileVel * projectileVel - sqrtDisc) / (gravity * dispVec.x));

        RaycastHit2D hitResult = Physics2D.Raycast(enemyInScene.transform.position, dispVec, 3, LayerMask.GetMask("Ground"));

        float[] validAngleArray = new float[] { angle1, angle2 };
        offensiveAim = validAngleArray.Length == 1 ? validAngleArray[0] : (hitResult.collider != null ? validAngleArray[0] : validAngleArray[1]);

        if (validAngles > 0)
        {
            if (_SHOOT)
            {
                _SHOOT = false;
                var bullet = Instantiate(_bulletPrefab, enemyInScene.transform.position, Quaternion.identity);
                Vector2 launchDir = new Vector2(Mathf.Cos(offensiveAim), Mathf.Sin(offensiveAim));

                launchDir *= Mathf.Sign(dispVec.x);

                bullet.GetComponent<Rigidbody2D>().AddForce(launchDir * firePower, ForceMode2D.Impulse);
            }

        }

        shootCooldown -= Time.deltaTime;
        if (shootCooldown <= 0)
        {
            shootCooldown = 1f;
            _SHOOT = true;
        }
    }
}
