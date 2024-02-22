using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float offsetY = 9f; // Offset value for the Y-axis
    public float offsetZ = 6f; // Offset value for the Z-axis
    public float minX = -10.7f; // Minimum X-coordinate
    public float maxX = -6.7f; // Maximum X-coordinate
    public float rotationAngle = 25f; // Rotation angle in degrees

    void Start()
    {
        // Rotate the camera downwards by the specified angle
        transform.Rotate(Vector3.right, rotationAngle);
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the desired X-coordinate within the specified range
            float newX = Mathf.Clamp(target.position.x, minX, maxX);

            // Calculate the desired position based on the player's position and offset
            Vector3 desiredPosition = new Vector3(newX, target.position.y + offsetY, target.position.z + offsetZ);

            // Interpolate smoothly between the current camera position and the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
        }
    }
}
