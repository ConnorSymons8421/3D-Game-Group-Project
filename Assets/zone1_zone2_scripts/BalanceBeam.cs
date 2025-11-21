using UnityEngine;

public class BalanceBeam : MonoBehaviour
{
    public float speed = 2f;
    public float tiltAngle = 20f;

    void Update()
    {
        // oscillate around 90 degrees so it stays lying down
        float tilt = Mathf.Sin(Time.time * speed) * tiltAngle;
        transform.rotation = Quaternion.Euler(0, 0, 90 + tilt);
    }
}