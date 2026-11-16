using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float height = 50f;
    public float speed = 5f;
    public float returnSpeed = 10f;
    public float returnDelay = 1.5f;   // seconds to wait before going back down

    private Rigidbody rb;
    private Vector3 startPosition;
    private bool playerOnPlatform = false;
    private float timeLeftPlatform = -10f;

    void Start()
    {
        // Moving platforms need a kinematic rigidbody so physics carries the player
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        startPosition = rb.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerOnPlatform = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            timeLeftPlatform = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (playerOnPlatform)
        {
            // Ride up while the player is on the platform
            Vector3 topPosition = startPosition + Vector3.up * height;
            rb.MovePosition(Vector3.MoveTowards(rb.position, topPosition, speed * Time.fixedDeltaTime));
        }
        else if (Time.time - timeLeftPlatform >= returnDelay)
        {
            // Return to the start so the player can use it again
            rb.MovePosition(Vector3.MoveTowards(rb.position, startPosition, returnSpeed * Time.fixedDeltaTime));
        }
    }
}