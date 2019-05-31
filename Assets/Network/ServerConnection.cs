using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ServerConnection : MonoBehaviour
{
    public string Host;
    public int Port;

    private readonly ClientWebSocket _webSocket = new ClientWebSocket();
    private readonly IList<Action<PlayerUpdate>> _callbacks = new List<Action<PlayerUpdate>>();

    async void Start()
    {
        await _webSocket.ConnectAsync(new Uri($"{Host}:{Port}"), CancellationToken.None);

        var buffer = new ArraySegment<byte>(new byte[8192]);
        while (_webSocket.State == WebSocketState.Open)
        {
            var res = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);
            if (res.EndOfMessage && buffer.Array != null)
            {
                var update = JsonUtility.FromJson(Encoding.ASCII.GetString(buffer.Array), typeof(PlayerUpdate)) as PlayerUpdate;
      
                foreach (var callback in _callbacks)
                {
                    callback(update);
                }
            }
        }
    }

    public void Register(Action<PlayerUpdate> callback) => _callbacks.Add(callback);

    public async void SendUpdate(WorldUpdate update)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            var messageBytes = Encoding.ASCII.GetBytes(JsonUtility.ToJson(update));
            await _webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
