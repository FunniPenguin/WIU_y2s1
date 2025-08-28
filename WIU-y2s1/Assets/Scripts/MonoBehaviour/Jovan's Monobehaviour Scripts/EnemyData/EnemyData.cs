using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Transform player;
    public Transform firePoint;
    public float attackCooldown = 2f;
    private float cooldownTimer;

    [HideInInspector] public float aimAngle;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    public bool IsCooldownReady() => cooldownTimer <= 0f;
    public void StartCooldown() => cooldownTimer = attackCooldown;
    public bool CanAttack() => IsCooldownReady();
}
// Made By Jovan Yeo Kaisheng
