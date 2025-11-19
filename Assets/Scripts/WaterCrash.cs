using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WaterCrash : MonoBehaviour
{
    [Header("Fail Settings")]
    public string failSceneName = "FailScreen";
    public float delayBeforeFail = 2f;

    [Header("Crash Text")]
    public GameObject crashCanvasPrefab; 
    public Vector3 textOffset = new Vector3(0, 2f, 2f); // Position of text relative to player

    [Header("Bobbing Settings")]
    public float bobAmplitude = 0.3f;
    public float bobFrequency = 2f;

    [Header("Splash Effect")]
    public ParticleSystem splashPrefab;

    private bool hasCrashed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCrashed && other.CompareTag("Player"))
        {
            hasCrashed = true;

            // Disable player movement
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = false;

            // Spawn the crash text in front of player
            GameObject canvasObj = Instantiate(crashCanvasPrefab);
            canvasObj.transform.position = other.transform.position + textOffset;
            canvasObj.transform.localScale = Vector3.one * 0.01f; // scale down for World Space

            // Set text inside canvas
            TMPro.TMP_Text tmpText = canvasObj.GetComponentInChildren<TMPro.TMP_Text>();
            if (tmpText != null)
            {
                tmpText.text = "You just CRASHED OUT!";
            }

            // Splash effect plays only once
            if (splashPrefab != null)
            {
                ParticleSystem splash = Instantiate(splashPrefab, other.transform.position, Quaternion.identity);
                splash.Play();
                Destroy(splash.gameObject, splash.main.duration + splash.main.startLifetime.constantMax);
            }

            // Start bobbing and fail sequence
            StartCoroutine(BobAndFail(other.transform, canvasObj));
        }
    }

    private IEnumerator BobAndFail(Transform player, GameObject canvasObj)
    {
        float timer = 0f;
        Vector3 startPos = player.position;

        while (timer < delayBeforeFail)
        {
            // Player bobbing only
            float yOffset = Mathf.Sin(timer * bobFrequency * Mathf.PI * 2f) * bobAmplitude;
            player.position = startPos + Vector3.down * 0.5f + Vector3.up * yOffset;

            // Keep crash text facing the camera
            if (Camera.main != null)
            {
                canvasObj.transform.LookAt(Camera.main.transform);
                canvasObj.transform.Rotate(0, 180, 0);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Remove the crash text and load fail screen
        Destroy(canvasObj);
        //SceneManager.LoadScene(failSceneName);
    }
}
