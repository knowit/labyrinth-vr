using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    public Text myIp;
    public Text boardMessage;
    public Text cameraMessage;

    void Start()
    {
        var ip = Dns.GetHostEntry(Dns.GetHostName())
            .AddressList
            .First(x => x.AddressFamily == AddressFamily.InterNetwork)
            .ToString();
        myIp.text = $"My ip: {ip}";

        boardMessage.text = $"Last board state: {null}";
        cameraMessage.text = $"Last ball state: {null}";
    }

    void Update()
    {
        var connection = this.GetIslpConnection();

        if (connection.BoardStateChannel != null && connection.BoardStateChannel.Message != null)
        {
            var vec = connection.BoardStateChannel.Message.Orientation;
            boardMessage.text = $"Last board state: x={vec.X.ToString("n2")} y={vec.Y.ToString("n2")}";
        }

        if (connection.BallStateChannel != null && connection.BallStateChannel.Message != null)
        {
            var vec = connection.BallStateChannel.Message.Position;
            cameraMessage.text = $"Last ball state: x={vec.X.ToString("n2")} y={vec.Y.ToString("n2")}";
        }
    }
}
