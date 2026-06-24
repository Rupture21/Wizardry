using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 20f;
    Transform player;
    EnemySpawner spawner;

    [Header("Damage Feedback")]
    public Color damageColor = new Color(1,0,0,1); //Color of damage flash
    public float damageFlashDuration = 0.2f;
    public float deathFadeTime = 0.6f; //Time before fade
    Color originalColor;
    SpriteRenderer sr;
    EnemyMovement movement;

    bool isDead = false;

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        spawner = FindAnyObjectByType<EnemySpawner>();
        
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        movement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (isDead) return;

        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg, Vector2 sourcePosition, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        if (isDead) return;

        currentHealth -= dmg;
        StartCoroutine(DamageFlash());

        if (dmg > 0)
        {
            GameManager.GenerateFloatingText(Mathf.FloorToInt(dmg).ToString(), transform);
        }

        if (knockbackForce > 0)
        {
            Vector2 dir = (Vector2)transform.position - sourcePosition;
            movement.Knockback(dir.normalized * knockbackForce, knockbackDuration);
        }

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    //Coroutine function that makes enemies flash when taking damage
    IEnumerator DamageFlash()
    {
        sr.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        sr.color = originalColor;
    }

    public void Kill()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;

        StartCoroutine(KillFade());
    }

    IEnumerator KillFade()
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0, origAlpha = sr.color.a;

        while (t < deathFadeTime)
        {
            yield return w;
            t += Time.deltaTime;

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 - t / deathFadeTime) * origAlpha);
        }

        if (spawner != null)
        {
            spawner.OnEnemyKilled();
        }

        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (isDead) return;

        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    // private void OnDestroy()
    // {
    //     EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
    //     es.OnEnemyKilled();
    // }

    void ReturnEnemy()
    {
        // EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        Transform spawnPoint = spawner.relativeSpawnPoints[Random.Range(0, spawner.relativeSpawnPoints.Count)];
        transform.position = player.position + spawnPoint.localPosition;
    }
}
