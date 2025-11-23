using UnityEngine;

public class StartPlatformTrigger : MonoBehaviour
{
    private bool hasStarted = false;

    private void OnTriggerExit(Collider other)
    {
        if (!hasStarted && other.CompareTag("Player"))
        {
            hasStarted = true;
            FindObjectOfType<GameTimer>().StartTimer();
        }
    }
}
