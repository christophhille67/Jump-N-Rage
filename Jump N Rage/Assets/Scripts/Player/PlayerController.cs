using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float airAcceleration = 15f;  // weaker air strafing    
    [SerializeField] private float turnSpeed = 720f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;
    [SerializeField] private float fallGravityMultiplier = 2f;     // falling is 2x faster
    [SerializeField] private float lowJumpGravityMultiplier = 3f;  // if released early, jump lower

    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private float groundCheckDistance = 0.7f;
    [SerializeField] private LayerMask groundLayers = ~0;

    private Rigidbody rb;
    private Transform cam;
    private Vector2 moveInput;
    private bool isGrounded;
    
    private float lastGroundedTime = -10f;    // when we touched the ground last
    private float lastJumpPressedTime = -10f; // when jump was pressed last
    private bool jumpHeld;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
        if (jumpAction.action.WasPressedThisFrame())
            lastJumpPressedTime = Time.time;
        
        jumpHeld = jumpAction.action.IsPressed();
    }

    private void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        // Remember when last on Ground
        if (isGrounded && rb.linearVelocity.y <= 0.1f)
            lastGroundedTime = Time.time;
        
        Vector3 moveDir = GetCameraRelativeDirection();

        Vector3 targetVelocity = moveDir * moveSpeed;

        Vector3 currentVelocity = rb.linearVelocity;
        currentVelocity.y = 0f;

        // On ground, strafing is sharp, in the air less so
        float currentAcceleration = isGrounded ? acceleration : airAcceleration;

        Vector3 newVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity,
                                                  currentAcceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(newVelocity.x, rb.linearVelocity.y, newVelocity.z);

        // Jump (with coyote time and jump buffer)
        bool wasRecentlyGrounded = Time.time - lastGroundedTime <= coyoteTime;
        bool jumpWasRecentlyPressed = Time.time - lastJumpPressedTime <= jumpBufferTime;

        if (wasRecentlyGrounded && jumpWasRecentlyPressed)
        {
            float jumpVelocity = Mathf.Sqrt(2f * -Physics.gravity.y * jumpHeight);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);

            // Use both up, so one press = one jump
            lastJumpPressedTime = -10f;
            lastGroundedTime = -10f;
        }

        // Better gravity
        if (rb.linearVelocity.y < 0f)
        {
            // falling: pull down harder
            rb.AddForce(Physics.gravity * (fallGravityMultiplier - 1f), ForceMode.Acceleration);
        }
        else if (rb.linearVelocity.y > 0f && !jumpHeld)
        {
            // still going up, but jump was released, jump is lower
            rb.AddForce(Physics.gravity * (lowJumpGravityMultiplier - 1f), ForceMode.Acceleration);
        }

        // Turn to direction we are moving
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, targetRotation,
                                                              turnSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);
        }
    }

    private Vector3 GetCameraRelativeDirection()
    {
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;
        return forward * moveInput.y + right * moveInput.x;
    }

    private bool CheckGrounded()
    {
        return Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down,
                                  out RaycastHit hit, groundCheckDistance,
                                  groundLayers, QueryTriggerInteraction.Ignore);
    }
}