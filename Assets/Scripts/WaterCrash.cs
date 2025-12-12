 using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Supercyan.FreeSample;

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
    private Coroutine bobbingCoroutine = null;

    public void ResetCrash()
    {
        hasCrashed = false;
        // Stop any running bobbing animation
        if (bobbingCoroutine != null)
        {
            StopCoroutine(bobbingCoroutine);
            bobbingCoroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCrashed && other.CompareTag("Player"))
        {
            hasCrashed = true;

            // Stop timer
            FindObjectOfType<GameTimer>().StopTimer();

            // Disable player movement
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = false;

            // UPDATED FOR NEW PLAYER
            SimpleSampleCharacterControl playerControl = other.GetComponent<SimpleSampleCharacterControl>();
            if (playerControl != null) playerControl.enabled = false;

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
            bobbingCoroutine = StartCoroutine(BobAndFail(other.transform, canvasObj));
        }
    }

    private IEnumerator BobAndFail(Transform player, GameObject canvasObj)
    {
        float timer = 0f;
        Vector3 startPos = player.position;
        // Keep player at water surface level (adjust this value if needed)
        float waterSurfaceOffset = 0.3f;

        while (timer < delayBeforeFail)
        {
            // Player bobbing only - bob around water surface instead of below
            float yOffset = Mathf.Sin(timer * bobFrequency * Mathf.PI * 2f) * bobAmplitude;
            player.position = new Vector3(startPos.x, startPos.y + waterSurfaceOffset + yOffset, startPos.z);

            // Keep crash text facing the camera
            if (Camera.main != null)
            {
                canvasObj.transform.LookAt(Camera.main.transform);
                canvasObj.transform.Rotate(0, 180, 0);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Remove the crash text and load retry screen
        Destroy(canvasObj);
        FindObjectOfType<RetryMenu>().ShowRetryMenu();
    }
}