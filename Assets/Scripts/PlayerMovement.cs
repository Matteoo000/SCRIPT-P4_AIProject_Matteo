using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float sprintMultiplier = 1.5f;
    public float groundCheckDistance = 0.1f; // This should be a small value for ground check
    public float obstacleCheckDistance = 1.1f; // This is for obstacle check
    public LayerMask groundLayer; // Ensure this is set to exclude the player's own layer
    public Transform cameraTransform; // Reference to the camera's transform

    private Rigidbody rb;
    private float originalSpeed;
    private Health health;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = speed;
        health = GetComponent<Health>();

        if (health == null)
        {
            Debug.LogError("Health component is missing on the player!");
        }

        // Ensure Rigidbody settings are correct
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned!");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void FixedUpdate()
    {
        CheckGroundedStatus();
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Get the direction relative to the camera
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Ensure the camera's forward and right vectors are parallel to the ground
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction
        Vector3 movement = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;

        movement *= speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintMultiplier;
        }

        // Pre-movement collision check using raycasting
        if (!Physics.Raycast(transform.position, movement.normalized, out RaycastHit hit, obstacleCheckDistance, groundLayer))
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void CheckGroundedStatus()
    {
        // Check if the player is grounded using raycasting
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance, groundLayer);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Example collision handling for taking damage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (health != null)
            {
                health.TakeDamage(10f); // Adjust damage as needed
            }
        }
    }
}
