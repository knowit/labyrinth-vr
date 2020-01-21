
using UnityEngine;

public class RemotePlayerController : MonoBehaviour
{
    public Transform trackingSpace;
    public Transform hmdTracker;

    public Vector3 PointOnBoard(Vector2 position, Transform board)
    {
        var ray = new Ray
        {
            origin = new Vector3(position.x, 100, position.y),
            direction = Vector3.down
        };

        return new Plane(-board.up, 0.5f).Raycast(ray, out var enter) 
            ? ray.GetPoint(enter)
            : new Vector3(position.x, 1.0f, position.y);

    }

    public Quaternion GetWantedOrientation()
    {
        var hmdLean = (hmdTracker.position - trackingSpace.position).normalized;
        var angle = Mathf.LerpAngle(2.0f, 0.0f, Mathf.Abs(Vector3.Dot(Vector3.up, hmdLean)));
        Debug.Log(angle);

        return Quaternion.AngleAxis(angle, hmdTracker.right);
    }
}
