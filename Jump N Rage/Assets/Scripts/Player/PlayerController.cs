using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float acceleration = 60f;    
    [SerializeField] private float turnSpeed = 720f;

    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;

    private Rigidbody rb;
    private Transform cam;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    private void OnEnable()  { moveAction.action.Enable(); }
    private void OnDisable() { moveAction.action.Disable(); }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = GetCameraRelativeDirection();

        Vector3 targetVelocity = moveDir * moveSpeed;

        Vector3 currentVelocity = rb.linearVelocity;
        currentVelocity.y = 0f;

        Vector3 newVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity,
                                                  acceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(newVelocity.x, rb.linearVelocity.y, newVelocity.z);

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
}