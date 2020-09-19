using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SceneProperty]
    public string mainMenuScene;

    [SceneProperty]
    public string labyrithScene;

    [SceneProperty]
    public string testScene;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //LoadMainMenu();
        LoadTestScene();
    }

    public void LoadTestScene() => SceneManager.LoadScene(testScene);
    public void LoadMainMenu() => SceneManager.LoadScene(mainMenuScene);
    public void LoadGame() => SceneManager.LoadScene(labyrithScene);   
}
