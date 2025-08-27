using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "_fireballAbility", menuName = "Scriptable Objects/_fireballAbility")]
public class _fireballAbility : _AbilityScript
{
    public GameObject _player;
    public GameObject _fireball;
    public float _fireballDamage = 5;
    public float _fireballSpeed = 5;
    public override void Ability()
    {
        Fire(_fireball);
    }

    private void Fire(GameObject _fireballProjectile)
    {
        var projectileSpawnGameObject = GameObject.FindGameObjectWithTag(_player.tag);
        var projectileSpawnPoint = projectileSpawnGameObject.transform.position;
        GameObject spawnObject = (GameObject)Instantiate(_fireballProjectile, projectileSpawnPoint, Quaternion.identity);

        //Spawning and setting fireball stuff
        spawnObject.SetActive(true);
        spawnObject.GetComponent<ProjectileMovement>()._fireballSpeed = _fireballSpeed;
        spawnObject.GetComponent<ProjectileMovement>()._fireballDamage = _fireballDamage;
        spawnObject.GetComponent<ProjectileMovement>().direction = projectileSpawnGameObject.GetComponent<_PlayerController>()._lastSavedDirection;

        Debug.Log(projectileSpawnGameObject.GetComponent<_PlayerController>()._lastSavedDirection);
    }
}
