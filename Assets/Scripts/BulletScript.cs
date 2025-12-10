using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public float bulletSpeed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * bulletSpeed;
    }
}
