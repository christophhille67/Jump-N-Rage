using UnityEngine;

public class ManualRespawn : MonoBehaviour
{
    public Transform spawnPoint;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (controller != null)
                controller.enabled = false;

            transform.position = spawnPoint.position;

            if (controller != null)
                controller.enabled = true;
        }
    }
}

