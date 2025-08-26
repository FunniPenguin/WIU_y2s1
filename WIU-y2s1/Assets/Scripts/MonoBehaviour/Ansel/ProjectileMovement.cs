using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float direction = 0;
    public float _fireballSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody2D>().linearVelocityX = direction * _fireballSpeed;
    }
}
