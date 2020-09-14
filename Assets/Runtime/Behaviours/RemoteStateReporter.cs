using UnityEngine;

public class RemoteStateReporter : MonoBehaviour
{
    public RemotePlayerController player;

    void LateUpdate()
    {
        var orientation = player.GetWantedOrientation();
        this.GetIslpConnection().JoystickStateChannel.Send(new JoystickState
        {
            Orientation = orientation.ToEulerRotationXZ()
        });
    }
}
