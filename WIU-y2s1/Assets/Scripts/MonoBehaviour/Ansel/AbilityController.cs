using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{
    public _AbilityScript _activeAbility;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var CastAction = InputSystem.actions.FindAction("Ability");

        CastAction.started += ctx =>
        {
            _activeAbility.Ability();
        };
    }

    //Ability acquired function goes here

    //Ability switch function goes here
}
