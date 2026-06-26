using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    private EnemyStats stats;
    private Transform player;
    private EnemyMovement movement;
    private float fireTimer;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        movement = GetComponent<EnemyMovement>();
        
        // Find the player safely
        PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
        if (playerStats != null) player = playerStats.transform;
    }

    void Update()
    {
        if (player == null || stats.currentHealth <= 0) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= stats.enemyData.AttackRange)
        {
            // 1. Tell movement script to stop moving (See Step 4)
            if (movement != null) movement.IsChasing = false; 

            // 2. Handle shooting cooldown
            fireTimer += Time.deltaTime;
            if (fireTimer >= stats.enemyData.FireRate)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
        else
        {
            // Out of range: resume moving toward player
            if (movement != null) movement.IsChasing = true;
        }
    }

    void Shoot()
    {
        if (stats.enemyData.ProjectilePrefab == null) return;

        // Calculate direction to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Spawn projectile
        GameObject projGo = Instantiate(stats.enemyData.ProjectilePrefab, transform.position, Quaternion.identity);
        EnemyProjectile projectile = projGo.GetComponent<EnemyProjectile>();

        if (projectile != null)
        {
            projectile.Setup(direction, stats.currentDamage);
        }
    }
}