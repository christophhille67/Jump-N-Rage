<<<<<<< HEAD
=======
using TMPro;
>>>>>>> 70b3a6e1911d818e35cbc071595f18438917daaf
using UnityEngine;

public class GameTimer : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
=======
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
>>>>>>> 70b3a6e1911d818e35cbc071595f18438917daaf
