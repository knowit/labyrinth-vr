
public interface IslpConnection
{
    InboundChannel<BallState> BallStateChannel { get; }
    InboundChannel<BoardState> BoardStateChannel { get; }
    OutboundChannel<JoystickState> JoystickStateChannel { get; }
    bool Ready { get; }
}
