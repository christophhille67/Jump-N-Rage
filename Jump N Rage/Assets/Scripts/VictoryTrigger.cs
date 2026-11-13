using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public GameObject victoryPanel;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger wurde berührt");
        
        if (other.CompareTag("Player"))
        {
            victoryPanel.SetActive(true);
        }
    }
}