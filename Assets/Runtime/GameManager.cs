using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform overviewPosition;
    public RemotePlayerController player;

    void Start()
    {
        player.Move(overviewPosition.position);
        GetComponent<ServerConnection>().Register(gu =>
        {
            switch(gu.Event)
            {
                case GameEvent.Playing:
                    StartGame();
                    break;
                case GameEvent.Finish:
                    RestartGame();
                    break;
            }
        });
    }

    public void StartGame()
    {
        gameObject.GetComponent<RemoteStateReporter>().active = true;
        gameObject.GetComponent<RemoteStateUpdater>().active = true;
    }

    public void RestartGame()
    {
        gameObject.GetComponent<RemoteStateReporter>().active = false;
        gameObject.GetComponent<RemoteStateUpdater>().active = false;

        player.Move(overviewPosition.position);
    }
}
