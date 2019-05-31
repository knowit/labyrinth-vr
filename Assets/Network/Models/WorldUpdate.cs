using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class WorldUpdate 
{
    [DataMember] public Quaternion Rotation { get; set; }
}
