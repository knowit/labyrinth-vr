using Google.Protobuf;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;


public class OutboundChannel<T> where T : IMessage<T>
{
    private readonly UdpClient _client;
    private readonly IPEndPoint _target;

    public OutboundChannel(string host, int port)
    {
        _target = new IPEndPoint(IPAddress.Parse(host), port);
        _client = new UdpClient();

        Debug.Log($"Cast {typeof(T).Name} to {_target.Address}:{_target.Port}");
    }

    public void Send(T message)
    {
        Task.Run(async () =>
        {
            using (var ms = new MemoryStream())
            {
                message.WriteTo(ms);
                var bytes = ms.ToArray();
                await _client.SendAsync(bytes, bytes.Length, _target);
            }
        });
    }
}

