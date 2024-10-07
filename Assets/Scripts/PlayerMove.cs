using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;            // Speed of the player movement
    public float jumpForce;            // Force applied when jumping (increased)
     public float jumpHeight = 3f; // Desired jump height in meters
    private float gravity = -9.81f; // Gravity in Unity

    [Header("Camera Settings")]
    public float mouseSensitivity = 3.0f;   // Sensitivity of camera rotation
    public float verticalRotationLimit = 80f; // Limit for vertical rotation
    public Transform cameraTransform;         // Reference to the camera transform

    [Header("Ground Check Settings")]
    public float groundCheckDistance = 0.1f; // Distance to check if grounded
    public LayerMask groundMask;             // Layer mask to define what is "ground"

    [Header("References")]
    public Animator animator;               // Reference to the Animator

    private Rigidbody rb;
    private bool isGrounded;
    private float verticalRotation = 0f; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the screen
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        GroundCheck();   // Check if grounded
        Move();
        RotatePlayer();  // Rotate player and camera together (left/right)
        Jump();
        AnimatePlayer();
    }

    private void Move()
    {
        // Get input from WASD keys
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down

        // Create a direction vector based on input
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Move the player in the direction of the input
        if (direction.magnitude >= 0.1f)
        {
            // Move the player forward/backward and strafe left/right
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            moveDirection.y = 0f; // Ignore vertical component
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void RotatePlayer()
    {
        // Get mouse input for horizontal rotation (Mouse X)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // Rotate the player horizontally based on Mouse X
        transform.Rotate(Vector3.up * mouseX);

        // Get mouse input for vertical rotation (Mouse Y)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY; // Change the vertical rotation

        // Clamp the vertical rotation to prevent flipping
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        // Rotate the camera vertically
        cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
    }

    private void Jump()
    {
        // Check if the player is grounded
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            float jumpForce = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y));
            Debug.Log("Jumping with force: " + jumpForce); // Log the jump force

            // Apply upward force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true); // Trigger jump animation
        }
    }

    private void GroundCheck()
    {
        // Perform a raycast to check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
        animator.SetBool("isGrounded", isGrounded); // Update animator parameter

        // Reset jump state if grounded
        if (isGrounded)
        {
            animator.SetBool("isJumping", false); // Stop jump animation when grounded
        }
    }

    private void AnimatePlayer()
    {
        // Determine whether the player is moving
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isWalking = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;

        // Update animator parameter for walking state
        animator.SetBool("isWalking", isWalking);

        // If not walking and not jumping, default to idle animation
        if (!isWalking && isGrounded)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the ground check ray in the scene view (optional)
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundCheckDistance);
    }
}
