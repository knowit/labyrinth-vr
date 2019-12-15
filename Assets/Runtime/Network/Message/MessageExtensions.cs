using UnityEngine;

public static class MessageExtensions
{
    public static Vector2 ToVector2Denormalized(this Vec2 vec2, Vector2 size)
        => new Vector2(
            Mathf.Lerp(-size.x, size.x, vec2.X),
            Mathf.Lerp(-size.y, size.y, vec2.Y));

    public static Quaternion ToQuaternion(this Vec2 vec2)
        => Quaternion.Euler(vec2.X, 0.0f, vec2.Y);

    public static Vec2 ToEulerRotationXZ(this Quaternion quat)
        => new Vec2
        {
            X = quat.eulerAngles.x,
            Y = quat.eulerAngles.z
        };
}

