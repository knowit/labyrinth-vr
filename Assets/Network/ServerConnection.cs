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
            var res = await _socketConnection.Receive<PlayerUpdate>();

            Debug.Log(res.Position);

            foreach (var callback in _callbacks)
            {
                callback(res);
            }
        }
    }

    public void Register(Action<PlayerUpdate> callback) => _callbacks.Add(callback);

    public async void SendUpdate(ControllerUpdate update)
    {
        if (_socketConnection != null)
        {
            await _socketConnection.Send(update);
        }
    }
}
