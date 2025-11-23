using UnityEngine;

public class PushBlock : MonoBehaviour
{
    public float speed = 2f;
    public float maxDistance = 4f;
    public float minSize = 1f;

    public bool reverseDirection = false;

    private Vector3 startPos;
    private Vector3 startScale;
    private float randomOffset;

    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
        randomOffset = Random.Range(0f, 5f);
        minSize = transform.localScale.z;
    }

    void Update()
    {
        float addedLength = Mathf.PingPong((Time.time + randomOffset) * speed, maxDistance);

        float newZScale = minSize + addedLength;
        transform.localScale = new Vector3(startScale.x, startScale.y, newZScale);

        float centerShift = addedLength / 2f;

        if (reverseDirection)
        {
            centerShift = -centerShift;
        }

        transform.localPosition = new Vector3(startPos.x, startPos.y, startPos.z + centerShift);
    }
}