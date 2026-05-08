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

    // Private variables
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;

    [HideInInspector]
    public bool gameOver = false;

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle player movement input
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Check if the player is grounded using a circle overlap check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;  // Set the jumping flag to true
        }
    }

    void FixedUpdate()
    {
        // Apply jump force in FixedUpdate to ensure consistent physics behavior
        if (isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = false;  // Reset the jumping flag
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Detect collision with an object tagged "End and set gameOver to true and stoping the player
        if (other.CompareTag("End"))
        {
            gameOver = true;
            moveSpeed = 0.0f;
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the ground check circle in the Unity Editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    
}
