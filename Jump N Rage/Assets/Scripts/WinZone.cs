using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [Header("UI & Effects")]
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
        Time.timeScale = 1f; // reset time scale to normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload the current scene
    }
}