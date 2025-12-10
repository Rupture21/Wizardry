using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public int health = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<BulletScript>(out BulletScript bullet))
        {
            health -= bullet.damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
