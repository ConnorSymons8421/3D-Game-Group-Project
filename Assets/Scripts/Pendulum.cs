using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float speed = 1f;
    public float tiltAngle = 25f;

    void Update()
    {
        float tilt = Mathf.Sin(Time.time * speed) * tiltAngle;
        transform.rotation = Quaternion.Euler(0, 0, tilt);
    }
}
