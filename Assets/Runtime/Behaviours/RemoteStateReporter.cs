using UnityEngine;

public class RemoteStateReporter : MonoBehaviour
{
    public RemotePlayerController player;
    private ServerConnection _connection;

    void Start()
    {
        _connection = this.GetServerConnection();
        if (!_connection)
        {
            Debug.LogError("Cant find a server connection");
        }
    }

    void LateUpdate()
    {
        var orientation = player.GetWantedOrientation();

        _connection.SendUpdate(new GameUpdate
        {
            Event = GameEvent.VrOrientation,
            Data = new GameMessage
            {
                VrOrientationUpdate = new VROrientationUpdate
                {
                    Orientation = orientation.ToEulerRotationXZ()
                }
            }
        });
    }
}
