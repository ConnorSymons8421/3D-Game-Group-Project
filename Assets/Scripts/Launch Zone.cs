using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchZone : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // check for Rigidbody
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //rb.AddForce(new Vector3(1, 1, 1) * bounceForce, ForceMode.Impulse); // send player flying forward
        }
    }
}
