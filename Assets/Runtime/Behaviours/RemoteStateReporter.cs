using UnityEngine;

public class RemoteStateReporter : MonoBehaviour
{
    public RemotePlayerController player;
    public bool active = false;

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
        if (!active)
            return;

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
