using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerUpdate : IByteSerializable
{
    public Vector2 Position;
    public Vector2 Rotation;

    public void Deserialize(MemoryStream stream)
    {
        var payload = new float[4];
        var length = payload.Length * 4;

        var bytes = stream.ToArray();

        Buffer.BlockCopy(bytes, 0, payload, 0, length);

        Position = new Vector2(payload[0], payload[1]);
        Rotation = new Vector2(payload[2], payload[3]);
    }

    public void Serialize(MemoryStream stream)
    {
        throw new NotImplementedException();
    }
}
