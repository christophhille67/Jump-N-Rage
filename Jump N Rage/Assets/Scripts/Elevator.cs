using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    public float height = 50f;
    public float speed = 5f;

    private Vector3 startPosition;
    private bool playerOnPlatform = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    void Update()
    {
        if (playerOnPlatform)
        {
            Vector3 targetPosition = startPosition + Vector3.up * height;

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }
    }
}
