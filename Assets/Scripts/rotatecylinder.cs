using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecylinder : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 15f;

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
