using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public InputActionReference attack;
    [SerializeField, Min(0f)] private float attackRate = 3f;
    private float nextAttackTime = 0f;

    public GameObject bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Aim
        Vector2 mouseScreenPosition2D = Mouse.current.position.ReadValue();

        Vector3 mouseScreenPosition = new(mouseScreenPosition2D.x, mouseScreenPosition2D.y, mainCam.nearClipPlane);
        mousePos = mainCam.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //Attack
        nextAttackTime += Time.deltaTime;

        if (attack != null && attack.action != null && attack.action.IsPressed())
        {
            if (attackRate > 0f)
            {
                if (nextAttackTime >= 1f / attackRate)
                {
                    Attack();
                    nextAttackTime = 0f;
                }
            }
        }
    }

    private void Attack()
    {
       Instantiate(bullet, transform.position, transform.rotation);
    }
}
