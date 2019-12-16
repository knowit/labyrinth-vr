using UnityEngine;
using UnityEngine.Events;

public class LabyrinthManager : MonoBehaviour
{
    void Start()
    {
        this.GetServerConnection(true)
            .onServerEvent.AddListener(new UnityAction<GameUpdate>(gu =>
            {
                switch (gu.Event)
                {
                    case GameEvent.Playing:
                        // TODO
                        break;
                    case GameEvent.Finish:
                        // TODO
                        break;
                }
            }));
    }
}
