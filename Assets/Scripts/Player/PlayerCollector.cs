using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D dectector;
    public float pullSpeed;

    private void Start()
    {
        player = GetComponentInParent<PlayerStats>();
    }

    public void SetRadius(float r)
    {
        if (!dectector) dectector = GetComponent<CircleCollider2D>();
        dectector.radius = r;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Pickup p)) //Check if the GameObject is a Pickup
        {
            p.Collect(player, pullSpeed);
        }
    }
}
