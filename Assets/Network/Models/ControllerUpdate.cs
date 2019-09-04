using System;
using System.IO;
using UnityEngine;

[Serializable]
public class ControllerUpdate : IByteSerializable
{
    public Vector2 AnalogeAxis;

    public void Deserialize(MemoryStream stream)
    {
        throw new NotImplementedException();
    }

    public void Serialize(MemoryStream stream)
    {
        float[] floats = new[] {AnalogeAxis.x, AnalogeAxis.y};
        var bytes = new byte[floats.Length * 4];
        Buffer.BlockCopy(floats, 0, bytes, 0, floats.Length * 4);
        stream.Write(bytes, 0, floats.Length * 4);
    }
}
