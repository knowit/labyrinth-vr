using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Google.Protobuf;
using UnityEngine;

public class SocketConnection
{
    private readonly Socket _connection;
    private readonly byte[] Buffer = new byte[1024];

    public SocketConnection(Socket connection)
    {
        _connection = connection;
    }

    public Task<int> Send(GameUpdate data)
    {
        var result = new TaskCompletionSource<int>();

        using (var ms = new MemoryStream())
        {
            data.WriteDelimitedTo(ms);

            var bytes = ms.ToArray();
            _connection.BeginSend(bytes, 0, bytes.Length, 0,
                ar =>
                {
                    result.TrySetResult(_connection.EndSend(ar));
                }, null);

            return result.Task;
        }
    }

    public async Task<GameUpdate> Receive()
    {
        var result = new TaskCompletionSource<MemoryStream>();
        using (var ms = new MemoryStream())
        {
            _connection.BeginReceive(Buffer, 0, 1024, SocketFlags.None, OnReceive, (result, ms));

            await result.Task;

            ms.Position = 0;
            return GameUpdate.Parser.ParseDelimitedFrom(ms);
        }
    }

    private void OnReceive(IAsyncResult ar)
    {
        var bytesRead = _connection.EndReceive(ar);
        var (result, ms) = ((TaskCompletionSource<MemoryStream>, MemoryStream))ar.AsyncState;

        if (bytesRead > 0)
        {
            ms.Write(Buffer, 0, bytesRead);
            Debug.Log(bytesRead);
            Debug.Log(Buffer);
            result.TrySetResult(ms);
        }
        else
        {
            _connection.BeginReceive(Buffer, 0, 1024, SocketFlags.None, OnReceive, (result, ms));
        }
    }
}

public class SocketClient
{
    public Task<SocketConnection> Connect(string host, int port)
    {
        var ipAddress = host == "localhost" ? Dns.GetHostEntry(host).AddressList.First() : IPAddress.Parse(host);
        var remoteEp = new IPEndPoint(ipAddress, port);

        var client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        
        var result = new TaskCompletionSource<SocketConnection>();
        client.BeginConnect(remoteEp, ar =>
        {
            client.EndConnect(ar);
            result.TrySetResult(new SocketConnection(client));
        }, null);
 
        return result.Task;
    }
}

