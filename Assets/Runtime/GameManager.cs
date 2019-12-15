using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameManager
{
    void LoadMainMenu();
    void LoadGame();
}

public class GameManagerMock : IGameManager
{
    public void LoadMainMenu()
    {
        Debug.Log("Load main menu");
    }
    public void LoadGame()
    {
        Debug.Log("Load game");
    }
}

public class GameManager : MonoBehaviour, IGameManager
{
    [SceneProperty]
    public string mainMenuScene;

    [SceneProperty]
    public string labyrithScene;

    void Start()
    {
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        if (SceneManager.GetSceneByName(labyrithScene).isLoaded)
        {
            SceneManager.UnloadSceneAsync(labyrithScene).completed += ap =>
            {
                SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Additive);
            };

            return;
        }

        SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        // Make sure a server connection is hot
        this.GetServerConnection(true);

        SceneManager.UnloadSceneAsync(mainMenuScene).completed += ap =>
        {
            SceneManager.LoadScene(labyrithScene, LoadSceneMode.Additive);
        };
    }
}
