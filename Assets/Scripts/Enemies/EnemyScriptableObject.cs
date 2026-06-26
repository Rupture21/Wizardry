using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyScriptableObject", menuName ="ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [Header("Ranged Enemy Settings")]
    [SerializeField] float attackRange;
    public float AttackRange { get => attackRange; private set => attackRange = value; }

    [SerializeField] float fireRate; // Time between shots in seconds
    public float FireRate { get => fireRate; private set => fireRate = value; }

    [SerializeField] GameObject projectilePrefab;
    public GameObject ProjectilePrefab { get => projectilePrefab; private set => projectilePrefab = value; }
}
