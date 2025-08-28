using UnityEngine;
using UnityEngine.Events;

public class AttackEventHandler : MonoBehaviour
{
    public GameObject attackPoint;
    public UnityEvent onAttack;
    public UnityEvent onAttackEnd;
    public void Attack()
    {
        attackPoint.SetActive(true);
        onAttack?.Invoke();
    }
    public void AttackEnd()
    {
        attackPoint.SetActive(false);
        onAttackEnd?.Invoke();
    }
}