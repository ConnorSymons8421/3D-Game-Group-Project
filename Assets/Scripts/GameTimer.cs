using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public bool timerRunning = false;

    private float timeElapsed = 0f;

    void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        float seconds = timeElapsed % 60f;

        timerText.text = $"{minutes:00}:{seconds:00.00}";
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public float GetTime()
    {
        return timeElapsed;
    }
}
