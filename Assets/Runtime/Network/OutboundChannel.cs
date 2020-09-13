using Google.Protobuf;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class OutboundChannel<T> where T : IMessage<T>
{
    private UdpClient _client;
    private IPEndPoint _ipEndPoint;

    public OutboundChannel(string host, int port)
    {
        _ipEndPoint = new IPEndPoint(
            host == "localhost" || host == GetLocalIPAddress() 
                ? IPAddress.Loopback 
                : IPAddress.Parse(host), 
            port);
        _client = new UdpClient();

        Debug.Log($"Cast {typeof(T).Name} to {host}:{port}");
    }

    public async Task Send(T message)
    {
        if (!_client.Client.Connected)
        {
            _client.Connect(_ipEndPoint);
        }

        using (var ms = new MemoryStream())
        {
            message.WriteDelimitedTo(ms);
            var bytes = ms.ToArray();
            await _client.SendAsync(bytes, bytes.Length);
        }
    }

    private static string GetLocalIPAddress()
        => Dns.GetHostEntry(Dns.GetHostName())
            .AddressList
            .First(x => x.AddressFamily == AddressFamily.InterNetwork)
            .ToString();
}

