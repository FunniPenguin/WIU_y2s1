using UnityEngine;

[CreateAssetMenu(fileName = "_fireballAbility", menuName = "Scriptable Objects/_fireballAbility")]
public class _fireballAbility : _AbilityScript
{
    public GameObject _player;
    public GameObject _fireball;
    public float _fireballDamage = 5;
    public float _fireballSpeed = 5;
    public float cooldown = 5;

    private float _timeLastHit = 0;
    public override void Ability()
    {
        _timeLastHit += Time.deltaTime;

        if (_timeLastHit < cooldown)
        {
            //Enter in fireball code here
            //Fireball needs to travel in a straight line at a constant linear velocity X
            Fire(_fireball);
            //Fireball collision should be handled in the fireball prefab itself

            _timeLastHit = 0;
        }
    }

    private void Fire(GameObject _fireballProjectile)
    {
        GameObject spawnObject = (GameObject)Instantiate(_fireballProjectile, _player.transform.position, Quaternion.identity);
        spawnObject.SetActive(true);

        spawnObject.GetComponent<Rigidbody2D>().linearVelocityX = _player.GetComponent<_PlayerController>()._lastSavedDirection * _fireballSpeed;
    }
}
