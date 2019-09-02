using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class PlayerUpdate
{
    [DataMember] public Vector2 Position;
    [DataMember] public Vector2 Rotation;
}
