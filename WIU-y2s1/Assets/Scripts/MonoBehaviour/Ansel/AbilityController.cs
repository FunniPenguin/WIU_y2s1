using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{
    public _AbilityScript _activeAbility;
    public float cooldown = 0.5f;
    private float _timeLastHit = 0;

    public List<_AbilityScript> _abilitiesInStore;

    public void Start()
    {
        if (_activeAbility == null)
        {
            _activeAbility = _abilitiesInStore[0];
        }
    }

    public void Update()
    {
        _timeLastHit += Time.deltaTime;
    }

    public void switchAbility()
    {
        for (int i = 0; i < _abilitiesInStore.Count; i++)
        {
            if (_activeAbility == _abilitiesInStore[i])
            {
                if (_abilitiesInStore[i] != _abilitiesInStore.LastOrDefault())
                {
                    _activeAbility = _abilitiesInStore[i + 1];
                    return;
                }
                else
                {
                    _activeAbility = _abilitiesInStore[0];
                    return;
                }
            }
            else
            {

            }
        }
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
