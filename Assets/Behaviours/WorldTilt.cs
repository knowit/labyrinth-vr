using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorldTilt : MonoBehaviour
{
    public float MaxTilt = 20;

    private Rigidbody _rigidbody;
    private CameraControl _cameraControl;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!_cameraControl)
        {
            _cameraControl = FindObjectOfType<CameraControl>();
        }

        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");

        var forward = _cameraControl?.transform.forward ?? Vector3.forward;
        var right = _cameraControl?.transform.right ?? Vector3.right;


        _rigidbody.MoveRotation(Quaternion.AngleAxis(MaxTilt * -dx, forward) * Quaternion.AngleAxis(MaxTilt * dy, right));
    }
}
