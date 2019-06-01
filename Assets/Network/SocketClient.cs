using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class SocketConnection
{
    private readonly Socket _connection;
    private readonly byte[] Buffer = new byte[1024];

    public SocketConnection(Socket connection)
    {
        _connection = connection;
    }

    public Task<int> Send(byte[] data)
    {
        var result = new TaskCompletionSource<int>();

        _connection.BeginSend(data, 0, data.Length, 0,
            ar =>
            {
                result.TrySetResult(_connection.EndSend(ar));
            }, null);

        return result.Task;
    }

    public Task<byte[]> Receive()
    {
        var result = new TaskCompletionSource<byte[]>();
        var response = new List<byte>();

        _connection.BeginReceive(Buffer, 0, 1024, SocketFlags.None, OnReceive, (result, response));

        return result.Task;
    }

    private void OnReceive(IAsyncResult ar)
    {
        var bytesRead = _connection.EndReceive(ar);
        var (result, response) = ((TaskCompletionSource<byte[]>, List<byte>))ar.AsyncState;

        if (bytesRead > 0)
        {
            response.AddRange(new ArraySegment<byte>(Buffer, 0, bytesRead));
            _connection.BeginReceive(Buffer, 0, 1024, SocketFlags.None, OnReceive, (result, response));
        }
        else
        {
            result.TrySetResult(response.ToArray());
        }
    }
}

public class SocketClient
{
    public Task<SocketConnection> Connect(string host, int port)
    {
        var ipAddress = Dns.GetHostEntry(host).AddressList.First();
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

