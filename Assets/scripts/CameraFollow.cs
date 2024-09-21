using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the target to follow
    public float smoothSpeed = 0.125f; // Speed of camera movement
    public Vector3 offset; // Offset of the camera from the target

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraFollow: Target not set!");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
