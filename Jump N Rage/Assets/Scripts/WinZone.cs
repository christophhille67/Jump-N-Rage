using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [Header("UI & Effects")]
    public GameObject winScreenUI;
    public GameObject blurVolume;

    public TMPro.TextMeshProUGUI rageText;

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
        if (rageText != null)
        
        rageText.text = "Rage count: " + RageCounter.RageCount.Count;
        
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {   
        RageCounter.RageCount.Count = 0;
        Time.timeScale = 1f; // reset time scale to normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload the current 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}

