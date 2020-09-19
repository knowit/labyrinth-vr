using System.Collections;
using UnityEngine;

public class IslpTester : MonoBehaviour
{
    public Transform Board;

    void Start()
    {
        IEnumerator Ticker()
        {
            while(true)
            {
                var connection = this.GetIslpConnection();

                if (connection.BoardStateChannel != null && connection.BoardStateChannel.Message != null)
                {
                    Board.rotation = connection.BoardStateChannel.Message.Orientation.ToQuaternion();
                }

                if (connection.JoystickStateChannel != null)
                {
                    var x = Input.GetAxis("Horizontal");
                    var y = Input.GetAxis("Vertical");

                    connection.JoystickStateChannel.Send(new JoystickState
                    {
                        Orientation = new Vec2 { X = x * 5.0f, Y = y * 4.0f }
                    });
                }
                yield return new WaitForSecondsRealtime(1.0f / 45.0f);
            }
        }
        StartCoroutine(Ticker());
    }
}
