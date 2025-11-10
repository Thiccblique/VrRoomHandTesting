using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables for movement and jump settings
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float gravityScale = 2.5f;  // Custom gravity scale

    // Private variables
    private Rigidbody rb;
    private bool isGrounded;
    private bool isJumping;

    [HideInInspector]
    public bool gameOver = false;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Handle player movement input
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, rb.velocity.z);

        // Check if the player is grounded using a sphere overlap check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;  // Set the jumping flag to true
        }
    }

    void FixedUpdate()
    {
        // Apply custom gravity
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);

        // Apply jump force in FixedUpdate to ensure consistent physics behavior
        if (isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isJumping = false;  // Reset the jumping flag
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect collision with an object tagged "End" and set gameOver to true and stop the player
        if (other.CompareTag("End"))
        {
            gameOver = true;
            moveSpeed = 0.0f;
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the ground check sphere in the Unity Editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
