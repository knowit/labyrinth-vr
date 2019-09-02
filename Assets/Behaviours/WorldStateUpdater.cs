using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateUpdater : MonoBehaviour
{
    public PlayerSpawner PlayerSpawner;
    public Rigidbody World;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        while (_updateQueue.Count > 0)
        {
            var update = _updateQueue.Dequeue();

            if (PlayerSpawner.Ball != null) 
                PlayerSpawner.Ball.MovePosition(new Vector3(update.Position.x, 0.0f, update.Position.y));

            World.MoveRotation(Quaternion.Euler(update.Rotation.x, 0.0f, update.Rotation.y));
        }
    }
}
