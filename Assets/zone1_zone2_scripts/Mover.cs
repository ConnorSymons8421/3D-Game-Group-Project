using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    // A dropdown list for easy selection
    public enum MovementAxis { LeftRight_X, UpDown_Y, ForwardBack_Z }

    [Header("Movement Settings")]
    public MovementAxis axis = MovementAxis.LeftRight_X; // Default
    public float speed = 2f;
    public float distance = 3f;

    private Rigidbody rb;
    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        // 1. Calculate the offset (Sine Wave)
        float offset = Mathf.Sin(Time.time * speed) * distance;
        Vector3 targetPos = startPos;

        // 2. Apply to the correct axis based on your Dropdown selection
        switch (axis)
        {
            case MovementAxis.LeftRight_X:
                targetPos = new Vector3(startPos.x + offset, startPos.y, startPos.z);
                break;
            case MovementAxis.UpDown_Y:
                targetPos = new Vector3(startPos.x, startPos.y + offset, startPos.z);
                break;
            case MovementAxis.ForwardBack_Z:
                targetPos = new Vector3(startPos.x, startPos.y, startPos.z + offset);
                break;
        }

        // 3. Move physically (Carries the player!)
        rb.MovePosition(targetPos);
    }
}