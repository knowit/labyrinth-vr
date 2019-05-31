using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Sensitivity = 720.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var dx = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        var dy = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;
        
        var position = transform.position;

        transform.RotateAround(position, transform.right, dy);
        transform.RotateAround(position, Vector3.up, dx);
    }
}
