using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Header("Settings")]
    public float bounceForce = 25f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                Vector3 currentVelocity = playerRb.velocity;

                playerRb.velocity = new Vector3(currentVelocity.x, bounceForce, currentVelocity.z);
            }
        }
    }
}