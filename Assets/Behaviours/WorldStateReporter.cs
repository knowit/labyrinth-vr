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
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");

        _connection.SendUpdate(new ControllerUpdate
        {
            AnalogeAxis = new Vector2(dx, dy)
        });
    }
}
