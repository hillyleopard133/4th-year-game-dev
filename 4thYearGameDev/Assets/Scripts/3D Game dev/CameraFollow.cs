using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 6f;
    public float sensitivity = 3f;
    public float minY = -30f;
    public float maxY = 60f;

    private float yaw;
    private float pitch = 20f;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Mouse input
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;

        pitch = Mathf.Clamp(pitch, minY, maxY);

        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Calculate position behind player
        Vector3 direction = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + direction;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}