
using UnityEngine;

public class RemotePlayerController : MonoBehaviour
{
    Vector3 PointOnBoard(Vector2 position)
    {
        // TODO: optimise with point-plane projection
        var ray = new Ray
        {
            origin = new Vector3(position.x, 100, position.y),
            direction = Vector3.down
        };
        Debug.DrawRay(ray.origin, ray.direction * 20);
        return Physics.Raycast(ray, out var hit) && hit.collider.name == "plate"
            ? hit.point + new Vector3(0.0f,0.2f,0.0f)
            : new Vector3(position.x, 1.0f, position.y);
    }

    public Quaternion GetWantedOrientation()
    {
        // TODO 
        return Quaternion.identity;
    }

    public void Move(Vector2 position)
    {
        transform.position = PointOnBoard(position);
    }


    public void Move(Vector3 position)
    {
        transform.position = position;
    }
}
