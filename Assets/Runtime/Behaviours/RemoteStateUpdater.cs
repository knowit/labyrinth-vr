using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteStateUpdater : MonoBehaviour
{
    public RemotePlayerController player;
    public Transform board;

    private ServerConnection _connection;
    private readonly Queue<LabyrinthStateUpdate> _updateQueue = new Queue<LabyrinthStateUpdate>();

    void Start()
    {
        _connection = this.GetServerConnection();
        if (!_connection)
        {
            Debug.LogError("Cant find a server connection");
            return;
        }

        _connection.Register(update => {
            if (update.Event == GameEvent.LabyrinthState)
            {
                _updateQueue.Enqueue(update.Data.LabyrinthStateUpdate);
            }
        });
    }

    void FixedUpdate()
    {
        while (_updateQueue.Count > 0)
        {
            var update = _updateQueue.Dequeue();
            
            var position = update.Position
                .ToVector2Denormalized(new Vector2(16,16));
            var rotation = update.BoardOrientation
                .ToQuaternion();

            player.Move(position);
            board.rotation = rotation;
        }
    }
}
