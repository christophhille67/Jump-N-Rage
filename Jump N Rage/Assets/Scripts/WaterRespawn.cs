using UnityEngine;

public class WaterRespawn : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RageCounter.RageCount.Count++;
            CharacterController controller =
                other.GetComponent<CharacterController>();

            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (controller != null)
            {
                controller.enabled = false;
            }

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            other.transform.position = spawnPoint.position;

            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }
}