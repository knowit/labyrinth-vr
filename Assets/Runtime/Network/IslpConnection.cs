using System;
using System.Net.Mqtt;
using UnityEngine;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Net.Sockets;

public class IslpConnection : MonoBehaviour
{
    public string MqttHost = "127.0.0.1";
    public int MqttPort = 1883;

    [Space]

    public int BasePort = 4049;

    private int _ballStatePort => BasePort;
    private int _boardStatePort => BasePort + 1;
    private int _joystickStatePort => BasePort + 2;

    private string _boardAddress = null;
    private string _cameraAddress = null;

    public InboundChannel<BallState> BallStateChannel { get; private set; } = null;
    
    public InboundChannel<BoardState> BoardStateChannel { get; private set; } = null;

    public OutboundChannel<JoystickState> JoystickStateChannel { get; private set; } = null;

    public bool Ready
        => BallStateChannel != null && BoardStateChannel != null && JoystickStateChannel != null;

    private Mqtt.SystemOnline _onlineMessage = new Mqtt.SystemOnline
    {
        address = GetLocalIPAddress()
    };

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

        BallStateChannel = new InboundChannel<BallState>(_ballStatePort);
        BoardStateChannel = new InboundChannel<BoardState>(_boardStatePort);
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

                    Debug.Log($"[VR] Message from board {_boardAddress}");

                    _mqttClient.PublishAsync(
                        _onlineMessage.AsMessage("labyrinth/vr/online"),
                        MqttQualityOfService.AtLeastOnce);

                    JoystickStateChannel = new OutboundChannel<JoystickState>(_boardAddress, _joystickStatePort);
                }
                break;

            case "labyrinth/camera/online":
                var cameraOnlineMsg = message.AsMessage<Mqtt.SystemOnline>();
                if (_cameraAddress != cameraOnlineMsg.address)
                {
                    _cameraAddress = cameraOnlineMsg.address;

                    Debug.Log($"[VR] Message from camera {_cameraAddress}");

                    _mqttClient.PublishAsync(
                        _onlineMessage.AsMessage("labyrinth/vr/online"),
                        MqttQualityOfService.AtLeastOnce);
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

