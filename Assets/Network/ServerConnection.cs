using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ServerConnection : MonoBehaviour
{
    public string Host = "localhost";
    public int Port = 9091;

    private readonly SocketClient _clientSocket = new SocketClient();
    private SocketConnection _socketConnection;
    private readonly IList<Action<PlayerUpdate>> _callbacks = new List<Action<PlayerUpdate>>();

    async void Start()
    {
        _socketConnection = await _clientSocket.Connect(Host, Port);

        while (gameObject.scene.isLoaded)
        {
            var res = await _socketConnection.Receive();
            var update = JsonUtility.FromJson(Encoding.ASCII.GetString(res), typeof(PlayerUpdate)) as PlayerUpdate;

            foreach (var callback in _callbacks)
            {
                callback(update);
            }
        }
    }

    public void Register(Action<PlayerUpdate> callback) => _callbacks.Add(callback);

    public async void SendUpdate(WorldUpdate update)
    {
        if (_socketConnection != null)
        {
            var messageBytes = Encoding.ASCII.GetBytes(JsonUtility.ToJson(update));
            await _socketConnection.Send(messageBytes);
        }
    }
}
