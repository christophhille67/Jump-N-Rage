using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float height = 50f;
    public float speed = 5f;

    private Rigidbody rb;
    private Vector3 startPosition;
    private bool playerOnPlatform = false;

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
            playerOnPlatform = false;
    }

    void FixedUpdate()
    {
        if (!playerOnPlatform)
            return;

        Vector3 targetPosition = startPosition + Vector3.up * height;
        rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime));
    }
}