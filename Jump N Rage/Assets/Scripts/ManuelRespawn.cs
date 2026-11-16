
using UnityEngine;
using TMPro;

public class ManualRespawn : MonoBehaviour
{
    public Transform spawnPoint;
    public TextMeshProUGUI respawnHint;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        // Hide the hint after 5 seconds
        if (respawnHint != null)
        {
            Invoke(nameof(HideHint), 5f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        if (spawnPoint == null) return;

        if (controller != null)
            controller.enabled = false;

        transform.position = spawnPoint.position;

        if (controller != null)
            controller.enabled = true;
    }

    private void HideHint()
    {
        respawnHint.gameObject.SetActive(false);
    }
}