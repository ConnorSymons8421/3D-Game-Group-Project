using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 15f; // increased default force

    void OnCollisionEnter(Collision collision)
    {
        // check for Rigidbody
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // reset gravity
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            Debug.Log("BOING! Bounced Player");
        }
    }
}