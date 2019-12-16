using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRKeyboardKey : UIBehaviour
{
    public Button.ButtonClickedEvent onClick => GetComponent<Button>().onClick;

    public void SetKey(KeyCode code)
    {
        GetComponentInChildren<Text>().text = code != KeyCode.Backspace 
            ? $"{(char)code}" 
            : "<";
    }
}
