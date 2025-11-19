using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -6);

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }
}

