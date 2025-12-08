using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition2D = Mouse.current.position.ReadValue();

        Vector3 mouseScreenPosition = new Vector3(mouseScreenPosition2D.x, mouseScreenPosition2D.y, mainCam.nearClipPlane);
        mousePos = mainCam.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
