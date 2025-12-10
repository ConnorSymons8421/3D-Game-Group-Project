using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweeperArm : MonoBehaviour
{
    //game object to spin around
    public GameObject pillar;
    public float speed = 30f;
    public int clockwise = 1; //to set if the column rotates clockwise or counterclockwise

    public float bounceForce = 30f;

    Vector3 direction;

    void Start()
    {
        direction = new Vector3(0, clockwise, 0);
    }

    void Update()
    {
        //spin object at constant speed
        transform.RotateAround(pillar.transform.position, direction, speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // check for Rigidbody
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(new Vector3(1, 1, 1) * bounceForce, ForceMode.Impulse); // send player flying away on contact
        }
    }
}
