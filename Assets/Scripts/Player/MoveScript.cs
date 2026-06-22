using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript :  MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    PlayerStats player;
    
    [HideInInspector]
    public Vector2 moveDirection;
    [HideInInspector]
    public float lastHorizontalvector;
    [HideInInspector]
    public float lastverticalVector;
    [HideInInspector]
    public Vector2 lastMovedVector;

    public InputActionReference move;

    void Start()
    {
        player = GetComponent<PlayerStats>();
        lastMovedVector = new Vector2(0f, -1);
    }

    private void Update()
    {
        InputMannagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputMannagement()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        moveDirection = move.action.ReadValue<Vector2>();

        if (moveDirection.x != 0)
        {
            lastHorizontalvector = moveDirection.x;
            lastMovedVector = new Vector2(lastHorizontalvector, 0f);
        }
        if (moveDirection.y != 0)
        {
            lastverticalVector = moveDirection.y;
            lastMovedVector = new Vector2(0f, lastverticalVector);
        }

        if (moveDirection.x != 0 && moveDirection.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalvector, lastverticalVector);
        }
    }

    void Move()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        rb.linearVelocity = new Vector2(moveDirection.x * player.CurrentMoveSpeed, moveDirection.y * player.CurrentMoveSpeed);
    }
}
