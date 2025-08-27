using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{
    public _AbilityScript _activeAbility;
    public float cooldown = 0.5f;
    private float _timeLastHit = 0;

    public void Update()
    {
        _timeLastHit += Time.deltaTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void callAbility()
    {
        if (_timeLastHit >= cooldown)
        {
            _activeAbility.Ability();

            _timeLastHit = 0;
        }
    }

    //Ability acquired function goes here

    //Ability switch function goes here
}
