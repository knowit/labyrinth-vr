using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdater : MonoBehaviour
{
    public Rigidbody Ball;
    
    void LateUpdate()
    {
        var playerController = FindObjectOfType<CameraMover>();
        playerController.Move(Ball.position);
    }
}
