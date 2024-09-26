using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float mouseSensitivity;
    public float jumpForce;
    // public Vector3 velocity;

    public Rigidbody rb;

    public Camera camera;
    public CharacterController characterController;
    float xRotation = 0f;
    public float jumpHeight;
    public float gravityValue = -9.81f;
    private Vector3 velocity;
    public AudioSource footclip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rigidbody from rotating due to physics

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
        footclip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        Jump();
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        // Get movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Get the forward and right direction relative to the camera
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        // Flatten the forward and right directions to the horizontal plane
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Normalize the vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on the input and camera direction
        Vector3 moveDirection = transform.forward  * moveZ + transform.right * moveX; 
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        if ((moveX != 0 || moveZ != 0) && IsGrounded())
        {
            if (!footclip.isPlaying) // Check if footstep audio is not already playing
            {
                footclip.Play(); // Play footstep sound
            }
        }
        else
        {
            footclip.Stop(); // Stop footstep sound if not moving
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        velocity.y += gravityValue * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        // Check if the player is on the ground by casting a ray downwards
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}