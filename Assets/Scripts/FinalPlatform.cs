using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

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

    [Header("Celebration Sounds")]
    public AudioSource smokeSFX;
    public AudioSource sparkSFX;
    public AudioSource confettiSFX;
    public AudioSource winSFX; 

    [Header("Camera Pan")]
    public Transform cameraTransform;     // Main Camera
    public float cameraDistance = 10f;     // How far behind the player
    public float cameraHeight = 4f;       // Height offset
    public float panDuration = 1.2f;      // How long the pan takes

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

            // Play effects + sounds
            if (buttonLight != null)
                buttonLight.intensity = 5f;

            if (smokeLeft != null)
            {
                smokeLeft.Play();
                if (smokeSFX != null) smokeSFX.Play();
            }

            if (smokeRight != null)
            {
                smokeRight.Play();
            }

            if (sparks != null)
            {
                sparks.Play();
                if (sparkSFX != null) sparkSFX.Play();
            }

            if (confetti != null)
            {
                confetti.Play();
                if (confettiSFX != null) confettiSFX.Play();
            }

            if (winSFX != null)
            {
                winSFX.Play();
            }

            if (cameraTransform != null)
            {
                foreach (MonoBehaviour mb in cameraTransform.GetComponents<MonoBehaviour>())
                {
                    mb.enabled = false;
                }
            }

            StartCoroutine(PanCameraBehindPlayer(other.transform));
            // Show win screen after delay
            Invoke(nameof(ShowWinScreen), winDelay);
        }
    }

    IEnumerator PanCameraBehindPlayer(Transform player)
    {
        if (cameraTransform == null) yield break;

        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;

        Vector3 targetPos =
            player.position
            - player.forward * cameraDistance
            + Vector3.up * cameraHeight;

        Quaternion targetRot =
            Quaternion.LookRotation(player.position + Vector3.up * 1.2f - targetPos);

        float elapsed = 0f;

        while (elapsed < panDuration)
        {
            float t = elapsed / panDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            cameraTransform.position = Vector3.Lerp(startPos, targetPos, t);
            cameraTransform.rotation = Quaternion.Slerp(startRot, targetRot, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = targetPos;
        cameraTransform.rotation = targetRot;
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
