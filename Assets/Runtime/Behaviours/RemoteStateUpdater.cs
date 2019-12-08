using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteStateUpdater : MonoBehaviour
{
    public RemotePlayerController player;
    public Transform board;
    public bool active = false;

    private ServerConnection _connection;
    private readonly Queue<LabyrinthStateUpdate> _updateQueue = new Queue<LabyrinthStateUpdate>();

    void Start()
    {
        _connection = FindObjectOfType<ServerConnection>();
        if (!_connection)
        {
            Debug.LogError("Cant find a server connection");
        }

        _connection.Register(update => {
            if (!active)
                return;

            if (update.Event == GameEvent.LabyrinthState)
            {
                _updateQueue.Enqueue(update.Data.LabyrinthStateUpdate);
            }
        });
    }

    void FixedUpdate()
    {
        if (!active)
            return;

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
