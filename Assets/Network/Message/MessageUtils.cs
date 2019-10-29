using UnityEngine;

public static class MessageUtils
{
    public static Vector2 ToUnityVector2(this Vec2 vec2)
    {
        return new Vector2(vec2.X, vec2.Y);
    }

    public static Vector3 ToUnityVector3XZ(this Vec2 vec2)
    {
        return new Vector3(vec2.X, 0.0f, vec2.Y);
    }

    public static Quaternion ToUnityQuaternionAsEulerRotationXZ(this Vec2 vec2)
    {
        return  Quaternion.Euler(vec2.X, 0.0f, vec2.Y);
    }
}

