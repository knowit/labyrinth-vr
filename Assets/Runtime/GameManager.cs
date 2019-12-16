using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameManager
{
    void LoadMainMenu();
    void LoadGame();
    void LoadConnectionTest();
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
    public void LoadConnectionTest()
    {
        Debug.Log("Load connection test");
    }
}

public class GameManager : MonoBehaviour, IGameManager
{
    [SceneProperty]
    public string mainMenuScene;

    [SceneProperty]
    public string labyrithScene;

    [SceneProperty]
    public string connectionTestScene;

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
        DontDestroyOnLoad(this.GetServerConnection().gameObject);
        SceneManager.LoadScene(labyrithScene);   
    }

    public void LoadConnectionTest()
    {
        SceneManager.LoadScene(connectionTestScene);
    }
}
