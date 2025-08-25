using UnityEngine;

[CreateAssetMenu(fileName = "stationary_action", menuName = "Scriptable Objects/stationary_action")]
public class stationary_action : StateAction
{
    private Animator animator;
    public override void Act(StateController controller)
    {
        var enemyInScene = GameObject.FindGameObjectWithTag("Enemy");
        animator = enemyInScene.GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
    }
}
