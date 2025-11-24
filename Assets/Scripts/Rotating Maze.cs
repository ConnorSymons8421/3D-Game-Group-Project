using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingMaze : MonoBehaviour
{
    //game object to rotate around
    public GameObject self;

    public float speed = 10f;

    void Update()
    {
        //spin object at constant speed
        transform.RotateAround(self.transform.position, Vector3.back, speed * Time.deltaTime);
    }
}
