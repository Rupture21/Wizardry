using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float lifespan = 0.5f;
    protected PlayerStats target;
    protected float speed;
    Vector2 initialPosition;
    float initialOffset;

    [System.Serializable]
    public struct BobbingAnimation
    {
        public float frequency;
        public Vector2 direction;
    }

    public BobbingAnimation bobbingAnimation = new BobbingAnimation
    {
        frequency = 2f, direction = new Vector2(0,0.3f)
    };

    [Header("Bonuses")]  
    public int experience;
    public int health;

    protected virtual void Start()
    {
        initialPosition = transform.position;
        initialOffset = Random.Range(0, bobbingAnimation.frequency);
    }

    public virtual void Update()
    {
        if (target)
        {
            speed += 5f * Time.deltaTime;

            Vector2 distance = target.transform.position - transform.position;

            float moveStep = speed * Time.deltaTime;
            if (distance.sqrMagnitude > moveStep * moveStep)
            {
                transform.position += (Vector3)distance.normalized * moveStep;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = initialPosition + bobbingAnimation.direction * Mathf.Sin((Time.time + initialOffset) * bobbingAnimation.frequency);
        }
    }

    public virtual bool Collect(PlayerStats target, float speed, float lifespan = 0f)
    {
        if (!this.target)
        {
            this.target = target;
            this.speed = speed;
            if (lifespan > 0) this.lifespan = lifespan;
            Destroy(gameObject, Mathf.Max(5f, this.lifespan));
            return true;
        }
        return false;
    }

    protected virtual void OnDestroy()
    {
        if (!target) return;
        if (experience != 0) target.IncreaseExperience(experience);
        if (health != 0) target.RestoreHealth(health);
    }
}
