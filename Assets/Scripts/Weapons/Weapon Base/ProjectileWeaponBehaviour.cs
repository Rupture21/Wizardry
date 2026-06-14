using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.AppUI.Core;
using Unity.VisualScripting;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    
    protected Vector3 direction;
    public float destroyAfterSeconds;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DiretionChecker(Vector3 dir)
    {
        // direction = dir;

        // float dirx = direction.x;
        // float diry = direction.y;

        // Vector3 scale = transform.localScale;
        // Vector3 rotation = transform.rotation.eulerAngles;

        // if(dirx < 0 && diry == 0)
        // {
        //     scale.x = scale.x * -1;
        //     scale.y = scale.y * -1;
        // }

        // transform.localScale = scale;
        // transform.rotation = Quaternion.Euler(rotation);
        direction = dir;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += -45;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        currentPierce --;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
