using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float sprintMultiplier = 1.5f;

    private Rigidbody rb;
    private float originalSpeed;
    private Health health;

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
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        Move();
        HandleJump();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = originalSpeed * sprintMultiplier;
        }
        else
        {
            speed = originalSpeed;
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // Check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
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
