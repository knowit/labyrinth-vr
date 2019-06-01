using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class PlayerUpdate
{
    [DataMember] public Vector3 Position;
}
