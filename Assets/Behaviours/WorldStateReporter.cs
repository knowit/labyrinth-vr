using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using UnityEngine;

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
        _connection.SendUpdate(new WorldUpdate
        {
            Rotation = transform.rotation
        });
    }
}
