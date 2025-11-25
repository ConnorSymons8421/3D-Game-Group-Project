using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    public GameObject retryMenuUI;
    public GameObject player;
    public GameTimer timer;

    private PlayerMovement movement;
    private PlayerCheckpointTracker checkpointTracker;

    private void Start()
    {
        movement = player.GetComponent<PlayerMovement>();
        checkpointTracker = player.GetComponent<PlayerCheckpointTracker>();
        retryMenuUI.SetActive(false);
    }

    public void ShowRetryMenu()
    {
        retryMenuUI.SetActive(true);

        movement.enabled = false;   // freeze player
        timer.StopTimer();          // pause timer when falling in water
    }

    public void Respawn()
    {
        retryMenuUI.SetActive(false);

        // Move player to last checkpoint
        player.transform.position = checkpointTracker.currentRespawnPoint.position;

          // Reset physics
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Re-enable movement
        movement.enabled = true;

        // Reset water crash so it can trigger again
        WaterCrash waterCrash = FindObjectOfType<WaterCrash>();
        if (waterCrash != null)
        {
            waterCrash.ResetCrash();
        }

        // Manually mark player as on checkpoint so timer resumes on exit
        checkpointTracker.SendMessage("OnRespawnOnCheckpoint", SendMessageOptions.DontRequireReceiver);
    }

    public void CrashOut()
    {
        SceneManager.LoadScene("FailMenu");
    }
}
