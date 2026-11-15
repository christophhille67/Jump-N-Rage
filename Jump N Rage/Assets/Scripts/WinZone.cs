using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [Header("UI & Effekte")]
    public GameObject winScreenUI;
    public GameObject blurVolume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        if (winScreenUI != null)
        {
            winScreenUI.SetActive(true);
        }

        if (blurVolume != null)
        {
            blurVolume.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // WICHTIG: Zeit wieder auf Normalgeschwindigkeit setzen!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Lädt die aktuelle Szene neu
    }
}