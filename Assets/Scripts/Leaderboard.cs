using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardPanel;   // Assign the panel
    public TMP_Text leaderboardText;      // Assign the TMP text

    private float[] topTimes = new float[3];

    void Start()
    {
        LoadTopTimes();
        leaderboardPanel.SetActive(false); // Hide on start
    }

    void LoadTopTimes()
    {
        for (int i = 0; i < 3; i++)
            topTimes[i] = PlayerPrefs.GetFloat($"TopTime{i}", 0f);
    }

    public void ShowLeaderboard()
    {
        LoadTopTimes(); // Reload in case times changed
        string text = "Conquer Ranking:\n";

        for (int i = 0; i < 3; i++)
        {
            if (topTimes[i] > 0)
                text += $"{i + 1}. {FormatTime(topTimes[i])}\n";
            else
                text += $"{i + 1}. --\n";
        }

        leaderboardText.text = text;
        leaderboardPanel.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }

    public void AddNewTime(float newTime)
    {
        for (int i = 0; i < 3; i++)
        {
            if (topTimes[i] == 0f || newTime < topTimes[i])
            {
                for (int j = 2; j > i; j--)
                    topTimes[j] = topTimes[j - 1];

                topTimes[i] = newTime;
                break;
            }
        }

        for (int i = 0; i < 3; i++)
            PlayerPrefs.SetFloat($"TopTime{i}", topTimes[i]);

        PlayerPrefs.Save();
    }

    // Converts seconds to MM:SS.HH format
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time % 60f;
        return $"{minutes:00}:{seconds:00.00}";
    }
}
