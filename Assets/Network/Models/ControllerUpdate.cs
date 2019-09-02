using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class ControllerUpdate
{
    [DataMember] public Vector2 AnalogeAxis;
}
