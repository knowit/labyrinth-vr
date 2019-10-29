﻿using UnityEngine;

public class WorldStateReporter : MonoBehaviour
{
    private ServerConnection _connection;

    void Start()
    {
        _connection = FindObjectOfType<ServerConnection>();
        if (!_connection)
        {
            Debug.LogError("Cant find a server connection");
        }
    }

    void LateUpdate()
    {
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");

        _connection.SendUpdate(new GameUpdate
        {
            Event = GameEvent.VrOrientation,
            Data = new GameMessage
            {
                VrOrientationUpdate = new VROrientationUpdate
                {
                    Orientation = new Vec2
                    {
                        X = dx,
                        Y = dy
                    }
                }
            }
        });
    }
}
