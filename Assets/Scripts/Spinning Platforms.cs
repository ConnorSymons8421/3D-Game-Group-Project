using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatforms : MonoBehaviour
{
    //game object to rotate around
    public GameObject self;

    public float speed = 25f;

    void Update()
    {
        //spin object at constant speed
        transform.RotateAround(self.transform.position, Vector3.back, speed * Time.deltaTime);
    }
}
