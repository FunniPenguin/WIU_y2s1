using UnityEngine;

[CreateAssetMenu(fileName = "still_action", menuName = "Scriptable Objects/still_action")]
public class still_action : StateAction
{
    private Animator animator;
    public override void Act(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("Enemy3");
        var playerInScene = GameObject.FindGameObjectWithTag("Player");
        animator = enemyInScene.GetComponent<Animator>();
        animator.SetBool("isMobile", false);
        //Debug.Log("Distance:" + (playerInScene.transform.position - enemyInScene.transform.position).magnitude.ToString());
    }
}
