using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody2D rb;

    public float bulletSpeed = 20f;

    public int damage = 20;

    private float lifetime = 2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
