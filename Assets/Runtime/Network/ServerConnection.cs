using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ServerConnection : MonoBehaviour
{
    public string Host { get; private set; }
    public int Port { get; private set; }

    public bool Connected => _socketConnection != null && _socketConnection.Connected;

    private readonly SocketClient _clientSocket = new SocketClient();
    private SocketConnection _socketConnection;

    public ServerEvent onServerEvent = new ServerEvent();

    void Awake()
    {
        Host = PlayerPrefs.GetString("host", "127.0.0.1");
        Port = PlayerPrefs.GetInt("port", 11000);
    }

    async void Start()
    {
        while (gameObject.activeSelf)
        {
            _socketConnection = await _clientSocket.Connect(Host, Port);
            if (_socketConnection != null && _socketConnection.Connected)
                break;
            await Task.Yield();
        }

        while (_socketConnection.Connected && gameObject.activeSelf)
        {
            var res = await _socketConnection.Receive();
            onServerEvent.Invoke(res);
        }
    }

    void OnDestroy()
    {
        if (_socketConnection != null)
            _socketConnection.Close();
    }

    public async void SendUpdate(GameUpdate update)
    {
        if (_socketConnection != null && _socketConnection.Connected)
        {
            await _socketConnection.Send(update);
        }
    }

    public class ServerEvent : UnityEvent<GameUpdate> { }
}
