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
        ipInputField.text = PlayerPrefs.GetString("host", "127.0.0.1");
        ipInputField.onEndEdit.AddListener(new UnityAction<string>(s =>
        {
            PlayerPrefs.SetString("host", s);
        }));

        portInputField.text = PlayerPrefs.GetInt("port", 11000).ToString();
        portInputField.onEndEdit.AddListener(new UnityAction<string>(s =>
        {
            PlayerPrefs.SetInt("port", int.Parse(s));
        }));

        startGameButton.onClick.AddListener(new UnityAction(() =>
        {
            this.GetGameManager().LoadConnectionTest();
        }));
    }
}
