using System;
using System.Net.Mqtt;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Net.Sockets;


public class IslpConnectionMqtt : MonoBehaviour, IslpConnection
{
    public string MqttHost = "127.0.0.1";
    public int MqttPort = 1883;

    [Space]

    public int BoardStatePort = 4049;
    public int JoystickStatePort = 4050;
    public int BallStatePort = 4051;

    public InboundChannel<BallState> BallStateChannel { get; private set; } = null;

    public InboundChannel<BoardState> BoardStateChannel { get; private set; } = null;

    public OutboundChannel<JoystickState> JoystickStateChannel { get; private set; } = null;

    public bool Ready
        => BallStateChannel != null && BoardStateChannel != null && JoystickStateChannel != null;

    private Mqtt.SystemOnline _onlineMessage = new Mqtt.SystemOnline
    {
        address = GetLocalIPAddress()
    };

    private string _boardAddress = null;
    private IMqttClient _mqttClient;

    async void Start()
    {
        _mqttClient = await MqttClient.CreateAsync(MqttHost, MqttPort);
        _mqttClient.MessageStream.Subscribe(message => OnMqttTopic(
            message.Topic,
            new Mqtt.MessageLoader { raw = message.Payload }));

        var session = await _mqttClient.ConnectAsync();
        if (session == SessionState.CleanSession)
        {
            await Task.WhenAll(
                _mqttClient.SubscribeAsync(
                    "labyrinth/board/online",
                    MqttQualityOfService.AtLeastOnce),
                _mqttClient.SubscribeAsync(
                    "labyrinth/camera/online",
                    MqttQualityOfService.AtLeastOnce)
                );
        }

        await _mqttClient.PublishAsync(
            _onlineMessage.AsMessage("labyrinth/vr/online"),
            MqttQualityOfService.AtLeastOnce);

        BallStateChannel = new InboundChannel<BallState>(BallStatePort);
        BoardStateChannel = new InboundChannel<BoardState>(BoardStatePort);
    }

    private void OnMqttTopic(string topic, Mqtt.MessageLoader message)
    {
        switch (topic)
        {
            case "labyrinth/board/online":
                var boardOnlineMsg = message.AsMessage<Mqtt.SystemOnline>();
                if (_boardAddress != boardOnlineMsg.address)
                {
                    _boardAddress = boardOnlineMsg.address;
                    JoystickStateChannel = new OutboundChannel<JoystickState>(_boardAddress, JoystickStatePort);
                }
                break;
        }
    }

    public static string GetLocalIPAddress()
        => Dns.GetHostEntry(Dns.GetHostName())
            .AddressList
            .First(x => x.AddressFamily == AddressFamily.InterNetwork)
            .ToString();
}
