using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "_dashAbility", menuName = "Scriptable Objects/_dashAbility")]
public class _dashAbility : _AbilityScript
{
    public GameObject _player;
    public float DashSpeed = 20;
    public float cooldown = 10;

    private Vector2 _dashTrajectory;

    public override void Ability()
    {
        Dash(DashSpeed);
    }

    private void Dash(float dashpower)
    {
        var playerInScene = GameObject.FindGameObjectWithTag(_player.tag);
        var body = playerInScene.GetComponent<Rigidbody2D>();

        _dashTrajectory.Set(dashpower * playerInScene.GetComponent<_PlayerController>()._lastSavedDirection, 5f);

        body.linearVelocity += _dashTrajectory;
    }
}
