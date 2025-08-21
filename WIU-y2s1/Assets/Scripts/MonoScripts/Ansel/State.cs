using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "_State", menuName = "Scriptable Objects/_State")]
public class _State : ScriptableObject
{
    public _StateAction[] actions;
    public _StateTransition[] transitions;

    public void UpdateState(_StateController controller)
    {
        DoActions(controller);
        CheckTransition(controller);
    }

    private void DoActions(_StateController controller)
    {
        foreach (_StateAction action in actions)
        {
            action.Act(controller);
        }
    }

    private void CheckTransition(_StateController controller) 
    {
        foreach (_StateTransition transition in transitions) 
        { 
            bool decisionSucceeded = transition.decision.Decide(controller);
            controller.TransitionToState(decisionSucceeded ? transition.truestate : transition.falsestate);
        }
    }
}
