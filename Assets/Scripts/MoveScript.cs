using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript :  MonoBehaviour
{
    public Rigidbody2D rb;

    public float moveSpeed;
    
    [HideInInspector]
    public Vector2 moveDirection;
    [HideInInspector]
    public float lastHorizontalvector;
    [HideInInspector]
    public float lastverticalVector;

    public InputActionReference move;

    private void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();

        if (moveDirection.x != 0)
        {
            lastHorizontalvector = moveDirection.x;
        }
        if (moveDirection.y != 0)
        {
            lastverticalVector = moveDirection.y;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

}
