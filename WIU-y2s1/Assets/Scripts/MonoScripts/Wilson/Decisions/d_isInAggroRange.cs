using UnityEngine;

[CreateAssetMenu(fileName = "d_isInAggroRange", menuName = "Scriptable Objects/d_isInAggroRange")]
public class d_isInAggroRange : StateDecision
{
    public GameObject enemyToTag;
    public GameObject playerToTag;
    public float aggroRange = 5.0f;

    
    private Vector2 toPlayer = Vector2.zero;

    public override bool Decide(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("ene1");
        var playerInScene = GameObject.FindGameObjectWithTag("player");
        toPlayer = playerInScene.transform.position - enemyInScene.transform.position;
        return (toPlayer.magnitude <= aggroRange);
    }
}
