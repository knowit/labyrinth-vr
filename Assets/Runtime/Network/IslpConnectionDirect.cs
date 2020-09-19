using UnityEngine;


public class IslpConnectionDirect : MonoBehaviour, IslpConnection
{
    public string BoardAddress = "127.0.0.1";

    [Space]

    public int BoardStatePort = 4049;
    public int JoystickStatePort = 4050;
    public int BallStatePort = 4051;

    public InboundChannel<BallState> BallStateChannel { get; private set; } = null;

    public InboundChannel<BoardState> BoardStateChannel { get; private set; } = null;

    public OutboundChannel<JoystickState> JoystickStateChannel { get; private set; } = null;

    public bool Ready
        => BallStateChannel != null && BoardStateChannel != null && JoystickStateChannel != null;

    void Start()
    {
        BallStateChannel = new InboundChannel<BallState>(BallStatePort);
        BoardStateChannel = new InboundChannel<BoardState>(BoardStatePort);

        JoystickStateChannel = new OutboundChannel<JoystickState>(BoardAddress, JoystickStatePort);
    }
}