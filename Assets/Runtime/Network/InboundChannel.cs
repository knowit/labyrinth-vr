using Google.Protobuf;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class InboundChannel<T> where T : IMessage<T>, new()
{
    public T Message { get; private set; }

    private UdpClient _listener;
    private MessageParser<T> _parser = new MessageParser<T>(() => new T());

    public InboundChannel(int listenPort)
    {
        _listener = new UdpClient(listenPort);
        Task.Run(Start);

        Debug.Log($"Listen for {typeof(T).Name} @ {listenPort}");
    }

    private async Task Start()
    {
        while (true)
        {
            Message = await Receive();
        }
    }

    private async Task<T> Receive()
    {
        using (var ms = new MemoryStream())
        {
            var result = await _listener.ReceiveAsync();
            ms.Write(result.Buffer, 0, result.Buffer.Length);

            var msg = _parser.ParseDelimitedFrom(ms);
            Debug.Log($"{typeof(T).Name} Recieved: {msg.ToString()}");
            return msg;
        }
    }
}

