using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPlatformTrigger : MonoBehaviour
{
    public Light buttonLight;
    public ParticleSystem smokeLeft;
    public ParticleSystem smokeRight;
    public ParticleSystem sparks;
    public ParticleSystem confetti;
    public float winDelay = 2.0f;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            // Light up button
            if (buttonLight != null)
                buttonLight.intensity = 5f;

            // Play effects
            if (smokeLeft != null) smokeLeft.Play();
            if (smokeRight != null) smokeRight.Play();
            if (sparks != null) sparks.Play();
            if (confetti != null) confetti.Play();

            // Show win screen after delay
            Invoke(nameof(ShowWinScreen), winDelay);
        }
    }

    void ShowWinScreen()
    {
        SceneManager.LoadScene("VictoryMenu");
    }
}
