using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdater : MonoBehaviour
{
    public Rigidbody Ball;
    public Transform Camera;
    
    void FixedUpdate()
    {
        Camera.position = Ball.position;
    }
}
