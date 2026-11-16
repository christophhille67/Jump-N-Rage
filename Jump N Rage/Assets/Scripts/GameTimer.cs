using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime;

    private void Update()
    {
        // Stops automatically when the win screen sets timeScale to 0
        elapsedTime += Time.deltaTime;

        int minutes = (int)(elapsedTime / 60f);
        float seconds = elapsedTime % 60f;
        timerText.text = $"{minutes:00}:{seconds:00.00}";
    }
}