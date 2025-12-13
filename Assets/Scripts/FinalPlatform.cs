using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalPlatformTrigger : MonoBehaviour
{
    [Header("Effects")]
    public Light buttonLight;
    public ParticleSystem smokeLeft;
    public ParticleSystem smokeRight;
    public ParticleSystem sparks;
    public ParticleSystem confetti;

    [Header("Timer")]
    public GameTimer timer;               // Assign your checkpoint-friendly GameTimer
    public TMP_Text fastestTimeText;      // Assign the UI text under the timer

    [Header("Win Settings")]
    public float winDelay = 2.0f;         // Delay before showing victory screen
    public string victorySceneName = "VictoryMenu";

    private bool activated = false;

    private void Start()
    {
        InitializeFastestTimeUI();
    }

    void InitializeFastestTimeUI()
    {
        if (fastestTimeText == null) return;

        float fastest = PlayerPrefs.GetFloat("TopTime0", 0f);

        if (fastest <= 0f)
        {
            fastestTimeText.text = "Fastest: --";
        }
        else
        {
            fastestTimeText.text = $"Fastest: {FormatTime(fastest)}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            // Stop the timer permanently
            timer.StopTimer();
            float newTime = timer.GetTime();

            // Update Top 3 leaderboard
            SaveBestTime(newTime);

            // Update fastest time UI
            UpdateFastestTimeUI();

            // Play effects
            if (buttonLight != null)
                buttonLight.intensity = 5f;
            if (smokeLeft != null) smokeLeft.Play();
            if (smokeRight != null) smokeRight.Play();
            if (sparks != null) sparks.Play();
            if (confetti != null) confetti.Play();

            // Show win screen after delay
            Invoke(nameof(ShowWinScreen), winDelay);
        }
    }

    void SaveBestTime(float newTime)
    {
        for (int i = 0; i < 3; i++)
        {
            float savedTime = PlayerPrefs.GetFloat($"TopTime{i}", 0f);

            if (savedTime == 0f || newTime < savedTime)
            {
                // Shift times down to make space
                for (int j = 2; j > i; j--)
                {
                    PlayerPrefs.SetFloat(
                        $"TopTime{j}",
                        PlayerPrefs.GetFloat($"TopTime{j - 1}", 0f)
                    );
                }

                PlayerPrefs.SetFloat($"TopTime{i}", newTime);
                break;
            }
        }

        PlayerPrefs.Save();
    }

    void UpdateFastestTimeUI()
    {
         float fastest = PlayerPrefs.GetFloat("TopTime0", 0f);

        if (fastestTimeText != null)
            fastestTimeText.text = fastest > 0f
                ? $"Fastest: {FormatTime(fastest)}"
                : "Fastest: --";
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time % 60f;
        return $"{minutes:00}:{seconds:00.00}";
    }

    void ShowWinScreen()
    {
        SceneManager.LoadScene(victorySceneName);
    }
}
