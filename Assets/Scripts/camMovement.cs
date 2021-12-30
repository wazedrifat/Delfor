using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour
{

    Vector3 Vec;
    public float speed;
   
    void Update()
    {
        Vec = transform.localPosition;
        Vec.x -= Input.GetAxis("Vertical")  * speed;
        Vec.z += Input.GetAxis("Horizontal")  * speed;
        transform.localPosition = Vec;
    }
}
