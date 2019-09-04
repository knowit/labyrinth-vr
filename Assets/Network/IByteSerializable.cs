
using System.IO;

public interface IByteSerializable
{
    void Serialize(MemoryStream stream);

    void Deserialize(MemoryStream stream);
} 

