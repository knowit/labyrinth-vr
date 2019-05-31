using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdater : MonoBehaviour
{
    public Rigidbody Ball;
    public Transform Camera;

    private ServerConnection _connection;
    private readonly Queue<PlayerUpdate> _updateQueue = new Queue<PlayerUpdate>();

    void Start()
    {
        _connection = FindObjectOfType<ServerConnection>();
        if (!_connection)
        {
            Debug.LogError("Cant find a server connection");
        }

        _connection.Register(update => _updateQueue.Enqueue(update));
    }
    
    void FixedUpdate()
    {
        Camera.position = Ball.position;

        while (_updateQueue.Count > 0)
        {
            Ball.MovePosition(_updateQueue.Dequeue().Position);
        }
    }
}
