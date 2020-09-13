
using System.Net.Mqtt;
using System.Text;
using UnityEngine;

public static class MqttMessageExtension
{
    public static byte[] AsBytes(this Mqtt.IMessage message)
        => Encoding.UTF8.GetBytes(JsonUtility.ToJson(message));

    public static MqttApplicationMessage AsMessage(this Mqtt.IMessage message, string topic)
        => new MqttApplicationMessage(topic, message.AsBytes());
}

namespace Mqtt
{
    public interface IMessage { }

    public class MessageLoader
    {
        public byte[] raw;
        public T AsMessage<T>() => JsonUtility.FromJson<T>(Encoding.UTF8.GetString(raw));
    }

    public struct SystemOnline : IMessage
    {
        public string address;
    }

    public struct NewGame : IMessage { }

    public struct RestartGame : IMessage { }
}

