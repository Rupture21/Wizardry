using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;
    SpriteRenderer sr;


    Vector2 knockbackVelocity;
    float knockbackDuration;

    public bool IsChasing { get; set; } = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindAnyObjectByType<MoveScript>().transform;
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else if (IsChasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
        }

        SpriteDirectionChecker();
    }

    public void Knockback(Vector2 velocity, float duration)
    {
        if (knockbackDuration > 0) return;

        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }

    void SpriteDirectionChecker()
    {
        // If the player is to the left of the enemy, face left
        if (player.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        // If the player is to the right of the enemy, face right
        else if (player.transform.position.x > transform.position.x)
        {
            sr.flipX = false;
        }
    }
}
