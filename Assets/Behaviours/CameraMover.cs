
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public void Move(Vector3 position)
    {
        transform.position = position;
        GetComponentInChildren<OVRCameraRig>().EnsureGameObjectIntegrity();
   
    }

    public void MoveAndRotate(Vector3 position, Quaternion quaternion)
    {
        transform.SetPositionAndRotation(position, quaternion);
        GetComponentInChildren<OVRCameraRig>().EnsureGameObjectIntegrity();
    }
}
