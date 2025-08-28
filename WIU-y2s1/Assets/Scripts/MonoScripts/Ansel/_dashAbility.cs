using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "_dashAbility", menuName = "Scriptable Objects/_dashAbility")]
public class _dashAbility : _AbilityScript
{
    public float DashSpeed = 20;
    public float cooldown = 10;

    private Vector2 _dashTrajectory;

    // Added by Jovan Yeo Kaisheng
    public AudioClip audio;

    public override void Ability()
    {
        Dash(DashSpeed);
    }

    private void Dash(float dashpower)
    {
        var playerInScene = GameObject.FindGameObjectWithTag("Player");
        var body = playerInScene.GetComponent<Rigidbody2D>();


        //_dashTrajectory.Set((dashpower * playerInScene.GetComponent<_PlayerController>()._lastSavedDirection), 5f);

        //body.linearVelocity += _dashTrajectory;

        body.linearVelocity = Vector2.zero;
        _dashTrajectory = new Vector2(playerInScene.GetComponent<_PlayerController>()._lastSavedDirection, 0).normalized;
        body.AddForce(_dashTrajectory * dashpower, ForceMode2D.Impulse);
        Debug.Log(body.linearVelocity);
    }
}
