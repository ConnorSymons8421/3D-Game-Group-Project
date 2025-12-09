using UnityEngine;

public class PlayerCheckpointTracker : MonoBehaviour
{
    public Transform currentRespawnPoint;
    public GameTimer timer;
    private bool onCheckpoint = false;

    private void Start()
    {
        // If no checkpoint is set, use player's starting position
        if (currentRespawnPoint == null)
        {
            currentRespawnPoint = transform;
            Debug.LogWarning("PlayerCheckpointTracker: No initial respawn point set. Using player start position.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            currentRespawnPoint = other.transform;
            onCheckpoint = true;
            Debug.Log("Checkpoint updated to: " + other.name);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Checkpoint") && onCheckpoint)
        {
            if (timer != null)
            {
                timer.StartTimer(); // resume timer when leaving platform
            }

            onCheckpoint = false;
        }
    }

    public void OnRespawnOnCheckpoint()
    {
        onCheckpoint = true;
    }
}
