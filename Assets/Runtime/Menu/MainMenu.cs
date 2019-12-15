using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        this.GetGameManager().LoadGame();
    }
}
