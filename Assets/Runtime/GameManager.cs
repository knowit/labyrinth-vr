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
        DontDestroyOnLoad(gameObject);
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(labyrithScene);   
    }
}
