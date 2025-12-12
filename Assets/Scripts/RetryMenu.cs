using Supercyan.FreeSample;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    public GameObject retryMenuUI;
    public GameObject player;
    public GameTimer timer;

    // private PlayerMovement movement;
    private SimpleSampleCharacterControl movement; // updated for new player
    private PlayerCheckpointTracker checkpointTracker;

    private void Start()
    {
        // movement = player.GetComponent<PlayerMovement>();
        movement = player.GetComponent<SimpleSampleCharacterControl>(); // updated for new player
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

        // Stop water crash bobbing first
        WaterCrash waterCrash = FindObjectOfType<WaterCrash>();
        if (waterCrash != null)
        {
            waterCrash.ResetCrash();
        }

        // Disable movement to clear input state
        movement.enabled = false;

        // Get Rigidbody and fully reset physics
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Move player to last checkpoint and reset rotation
        if (checkpointTracker.currentRespawnPoint != null)
        {
            player.transform.position = checkpointTracker.currentRespawnPoint.position;
            player.transform.rotation = checkpointTracker.currentRespawnPoint.rotation;
        }
        else
        {
            Debug.LogError("RetryMenu: No respawn point set! Cannot respawn player.");
            return;
        }

        // Small delay to ensure physics settle, then re-enable
        StartCoroutine(ReenablePlayerAfterRespawn(rb));

        // Manually mark player as on checkpoint so timer resumes on exit
        checkpointTracker.SendMessage("OnRespawnOnCheckpoint", SendMessageOptions.DontRequireReceiver);
    }

    private System.Collections.IEnumerator ReenablePlayerAfterRespawn(Rigidbody rb)
    {
        // Wait one physics frame
        yield return new WaitForFixedUpdate();
        
        // Re-enable physics first
        if (rb != null)
        {
            rb.isKinematic = false;
            // Now clear velocities (must be after isKinematic = false)
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Re-enable movement after physics settled
        movement.enabled = true;
    }

    public void CrashOut()
    {
        SceneManager.LoadScene("FailMenu");
    }
}