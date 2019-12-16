using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startGameButton;
    public VRInputField ipInputField;
    public VRInputField portInputField;

    void Start()
    {
        PlayerPrefs.DeleteAll();

        ipInputField.text = PlayerPrefs.GetString("host", "127.0.0.1");
        ipInputField.onEndEdit.AddListener(new UnityAction<string>(s =>
        {
            Debug.Log($"IP Field updated: {s}");
            PlayerPrefs.SetString("host", s);
        }));

        portInputField.text = PlayerPrefs.GetInt("port", 11000).ToString();
        portInputField.onEndEdit.AddListener(new UnityAction<string>(s =>
        {
            Debug.Log($"Port Field updated: {s}");
            PlayerPrefs.SetInt("port", int.TryParse(s, out var val) ? val : 11000);
        }));

        startGameButton.onClick.AddListener(new UnityAction(() =>
        {
            this.GetGameManager().LoadConnectionTest();
        }));
    }
}
