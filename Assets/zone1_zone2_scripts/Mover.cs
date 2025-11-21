using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float distance = 3f;
    public bool moveOnZAxis = true;

    private Vector3 startPos;
    private float randomOffset;
    private Rigidbody rb; 
    void Start()
    {
        startPos = transform.position;
        randomOffset = Random.Range(0f, 10f);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() 
    {
        float move = Mathf.Sin((Time.time + randomOffset) * speed) * distance;
        Vector3 targetPosition;

        if (moveOnZAxis)
        {
            targetPosition = new Vector3(startPos.x, startPos.y, startPos.z + move);
        }
        else
        {
            targetPosition = new Vector3(startPos.x + move, startPos.y, startPos.z);
        }

        if (rb != null)
        {
            rb.MovePosition(targetPosition);
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}