using Google.Protobuf;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class InboundChannel<T> where T : IMessage<T>, new()
{
    public T Message { get; private set; }

    private readonly UdpClient _listener;
    private readonly MessageParser<T> _parser = new MessageParser<T>(() => new T());

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
            await Task.Yield();
        }
    }

    private async Task<T> Receive()
    {
        while (true)
        {
            try
            {
                var result = await _listener.ReceiveAsync();
                return _parser.ParseFrom(result.Buffer);
            }
            catch (InvalidProtocolBufferException e)
            {
                Debug.LogError(e.Message);
            }
            catch (SocketException e)
            {
                Debug.LogError(e.Message);
                throw e;
            }
            await Task.Yield();
        }
    }
}

