using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateUpdater : MonoBehaviour
{
    public PlayerSpawner PlayerSpawner;
    public Rigidbody World;

    private ServerConnection _connection;
    private readonly Queue<GameUpdate> _updateQueue = new Queue<GameUpdate>();

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

            var position = update.Data.LabyrinthStateUpdate.Position.ToUnityVector3XZ();
            var rotation = update.Data.LabyrinthStateUpdate.BoardOrientation.ToUnityQuaternionAsEulerRotationXZ();
            
            Debug.Log(position);
            Debug.Log(rotation);

            if (PlayerSpawner.Ball != null) 
                PlayerSpawner.Ball.MovePosition(position);

            World.MoveRotation(rotation);
        }
    }
}
