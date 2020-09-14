using UnityEngine;

public class RemoteStateUpdater : MonoBehaviour
{
    public RemotePlayerController player;
    public Transform board;


    void FixedUpdate()
    {
        var connection = this.GetIslpConnection();
        var ballState = connection.BallStateChannel.Message;
        var boardState = connection.BoardStateChannel.Message;

        var position = ballState.Position
            .ToVector2Denormalized(new Vector2(16, 16));
        var rotation = boardState.Orientation
            .ToQuaternion();

        player.transform.position = player.PointOnBoard(position, board);
        board.rotation = rotation;
    }
}
