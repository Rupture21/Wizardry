// using Unity.AppUI.UI;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody2D rb;

    public float bulletSpeed = 20f;

    public int damage = 20;

    private float lifetime = 2.5f;

    private bool isBouncingEnabled = true;
    private Vector3 incomingDir;
    private Vector2 incomingDir2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        incomingDir2D = new Vector2(transform.right.x, transform.right.y).normalized;
        rb.linearVelocity = incomingDir2D * bulletSpeed;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBouncingEnabled)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflectDir = Vector2.Reflect(incomingDir2D, normal);
            float angle = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            incomingDir2D = reflectDir;
            rb.linearVelocity = reflectDir.normalized * bulletSpeed;
        }
    }
}
