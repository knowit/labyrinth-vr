using UnityEngine;

public class LabyrinthManager : MonoBehaviour
{
    void Start()
    {
        this.GetServerConnection(true)
            .Register(gu =>
            {
                switch(gu.Event)
                {
                    case GameEvent.Playing:
                        // TODO
                        break;
                    case GameEvent.Finish:
                        // TODO
                        break;
                }
            });
    }
}
