using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    // Public variables for movement and jump settings
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float gravityScale = 2.5f; // Custom Gravity

    // Private variables
    private Rigidbody rb;
    private bool isGrounded;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody compenet attached to the player
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player movement via velocity in rb
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, rb.velocity.z);

        //Check is the player is grounded using a sphere overlap check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        
        //Handle jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

    }

    private void FixedUpdate()
    {
        //Apply custom gravity
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);

        // Apply jump force in FixedU[pdate to ensure consistint physics behavior
        if(isJumping)
        {
            rb.velocity  = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isJumping = false; // Resers the jmping flag
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check sphere in the Unity Editor
        if(groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
