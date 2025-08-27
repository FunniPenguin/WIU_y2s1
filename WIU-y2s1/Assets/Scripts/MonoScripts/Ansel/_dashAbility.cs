using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "_dashAbility", menuName = "Scriptable Objects/_dashAbility")]
public class _dashAbility : _AbilityScript
{
    public GameObject _player;
    public float DashSpeed = 20;
    public float cooldown = 10;

    public override void Ability()
    {
        Dash(DashSpeed);
    }

    private void Dash(float dashpower)
    {
        var playerInScene = GameObject.FindGameObjectWithTag(_player.tag);
        var body = playerInScene.GetComponent<Rigidbody2D>();

        body.linearVelocityX = dashpower * playerInScene.GetComponent<_PlayerController>()._lastSavedDirection;
        Debug.Log(body.linearVelocityX);
        body.linearVelocityY = 0;
    }
}
