using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "Scriptable Objects/State")]
public class State : ScriptableObject
{
    public StateAction[] actions;
    public StateTransition[] transitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransition(controller);
    }

    private void DoActions(StateController controller)
    {
        foreach (StateAction action in actions)
        {
            action.Act(controller);
        }
    }

    private void CheckTransition(StateController controller) 
    {
        foreach (StateTransition transition in transitions) 
        { 
            bool decisionSucceeded = transition.decision.Decide(controller);
            controller.TransitionToState(decisionSucceeded ? transition.truestate : transition.falsestate);
        }
    }
}
