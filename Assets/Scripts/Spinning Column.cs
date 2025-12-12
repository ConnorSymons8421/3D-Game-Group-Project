using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningColumn : MonoBehaviour
{
    //game object to spin around
    public GameObject self;
    public float speed = 10f;
    public int clockwise = 1; //to set if the column rotates clockwise or counterclockwise

    Vector3 direction;

    void Start()
    {
        direction = new Vector3(0, clockwise, 0);
    }

    void Update()
    {
        //spin object at constant speed
        transform.RotateAround(self.transform.position, direction, speed * Time.deltaTime);
    }
}
