using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 8f;
    public float gravity = -20f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    public float coyoteTime = 0.15f; // small grace period after leaving ground

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float coyoteCounter;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Debug.Log("PlayerMovement Start: script initialized on " + gameObject.name);
        if (controller == null)
            Debug.LogWarning("PlayerMovement: No CharacterController found on " + gameObject.name + ". Movement will not work.");
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Ground check using a small sphere at the feet (more reliable than CharacterController skin-based grounded)
        if (groundCheck != null)
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        else
            isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // keeps player grounded
        }

        // update coyote counter
        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        // Accept jump from Input Manager "Jump" button or Space key directly
        bool jumpPressed = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space);
        // Allow jump if within coyote time (short grace after leaving ground)
        if (jumpPressed && coyoteCounter > 0f)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            coyoteCounter = 0f;
            Debug.Log("Jump pressed and executed");
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
