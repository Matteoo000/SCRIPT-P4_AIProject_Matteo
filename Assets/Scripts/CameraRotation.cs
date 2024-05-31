using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 100.0f;  // Speed of the camera rotation
    public bool useMouse = true;          // Use mouse for rotation
    public bool useArrowKeys = true;      // Use arrow keys for rotation
    public float minVerticalAngle = -60f; // Minimum vertical angle (looking down)
    public float maxVerticalAngle = 60f;  // Maximum vertical angle (looking up)
    public float minHorizontalAngle = -90f; // Minimum horizontal angle (left)
    public float maxHorizontalAngle = 90f;  // Maximum horizontal angle (right)

    private float verticalRotation = 0.0f;
    private float horizontalRotation = 0.0f;

    void Start()
    {
        // Initialize the horizontal and vertical rotations to the current rotation
        Vector3 currentRotation = transform.localEulerAngles;
        horizontalRotation = currentRotation.y;
        verticalRotation = currentRotation.x;
    }

    void Update()
    {
        float rotationX = 0f;
        float rotationY = 0f;

        // Rotation using arrow keys
        if (useArrowKeys)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotationY = -rotationSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rotationY = rotationSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rotationX = -rotationSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                rotationX = rotationSpeed * Time.deltaTime;
            }
        }

        // Rotation using mouse
        if (useMouse)
        {
            rotationY = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            rotationX = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        }

        // Apply rotation
        verticalRotation = Mathf.Clamp(verticalRotation + rotationX, minVerticalAngle, maxVerticalAngle);
        horizontalRotation = Mathf.Clamp(horizontalRotation + rotationY, minHorizontalAngle, maxHorizontalAngle);

        transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);
    }
}
