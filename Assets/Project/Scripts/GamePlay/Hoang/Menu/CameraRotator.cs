using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Transform target; 
    public float rotationSpeed = 50f; 
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        transform.RotateAround(target.position, rotationAxis, rotationSpeed * Time.deltaTime);

        //if (Input.GetMouseButton(0))
        //{
        //    float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        //    transform.RotateAround(target.position, Vector3.up, mouseX); 
        //}
    }
}
