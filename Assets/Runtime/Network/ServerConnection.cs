using System;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnection : MonoBehaviour
{
    public string Host = "localhost";
    public int Port = 9091;

    private readonly SocketClient _clientSocket = new SocketClient();
    private SocketConnection _socketConnection;
    private readonly IList<Action<GameUpdate>> _callbacks = new List<Action<GameUpdate>>();

    async void Start()
    {
        _socketConnection = await _clientSocket.Connect(Host, Port);

        while (_socketConnection.Connected)
        {
            var res = await _socketConnection.Receive();

            foreach (var callback in _callbacks)
            {
                callback(res);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (_socketConnection != null)
            _socketConnection.Close();
    }

    public void Register(Action<GameUpdate> callback) => _callbacks.Add(callback);

    public async void SendUpdate(GameUpdate update)
    {
        if (_socketConnection != null && _socketConnection.Connected)
        {
            await _socketConnection.Send(update);
        }
    }
}
