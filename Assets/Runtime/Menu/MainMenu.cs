using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startGameButton;

    void Start()
    {
        startGameButton.onClick.AddListener(new UnityAction(() =>
        {
            this.GetGameManager().LoadGame();
        }));
    }
}
