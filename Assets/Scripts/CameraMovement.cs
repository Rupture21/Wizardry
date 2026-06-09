using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    // public float cameraSmoothingAmount = 25.0f;
    // private Vector3 currentVelocity;
    void Update()
    {
        transform.position = target.position + offset;
        // Vector3 targetPosition = target.position + offset;
        // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, cameraSmoothingAmount * Time.deltaTime);
    }
}
