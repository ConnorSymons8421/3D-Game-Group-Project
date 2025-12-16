using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Header("Settings")]
    public float bounceForce = 25f;
    private AudioSource audioSource; // Variable to hold the speaker

    void Start()
    {
        // automatically find the Audio Source on this object
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                if (audioSource != null)
                {
                    audioSource.Play();
                }

                Vector3 currentVelocity = playerRb.velocity;
                playerRb.velocity = new Vector3(currentVelocity.x, bounceForce, currentVelocity.z);
            }
        }
    }
}