using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckConnection : MonoBehaviour
{
    public Text text;

    private ServerConnection _connection;

    void Start()
    {
        _connection = this.GetServerConnection(true);
        text.text = $"Connecting to {_connection.Host}:{_connection.Port}";

        _connection.onServerEvent.AddListener(new UnityAction<GameUpdate>(update =>
        {
            if (update.Event == GameEvent.Playing || update.Event == GameEvent.VrOrientation)
            {
                this.GetGameManager().LoadGame();
            }
            else if (update.Event == GameEvent.Finish)
            {
                Destroy(_connection.gameObject);
                this.GetGameManager().LoadMainMenu();
            }
        }));
    }

    void Update()
    {
        if (_connection.Connected)
        {
            text.text = $"Connected to {_connection.Host}:{_connection.Port}, waiting for event";
        }
        
        if (Time.timeSinceLevelLoad > 10)
        {
            Destroy(_connection.gameObject);
            this.GetGameManager().LoadMainMenu();
        }
    }
}
