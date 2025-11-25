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
    public float coyoteTime = 0.15f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float coyoteCounter;

    // 🔹 NEW: Reference to Animator
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();  // capsule child or same object

        Debug.Log("PlayerMovement Start: script initialized on " + gameObject.name);
        if (controller == null)
            Debug.LogWarning("PlayerMovement: No CharacterController found on " + gameObject.name);
        if (animator == null)
            Debug.LogWarning("PlayerMovement: No Animator found!");
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // // 🔹 NEW: Update animator speed parameter
        // if (animator != null)
        //     animator.SetFloat("Speed", move.magnitude);

        // 🔹 ground check
        if (groundCheck != null)
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        else
            isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        bool jumpPressed = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space);

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
