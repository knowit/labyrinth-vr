using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRKeyboardKey : UIBehaviour
{
    public Button.ButtonClickedEvent onClick => GetComponent<Button>().onClick;

    public void SetKey(KeyCode code)
    {
        if (code != KeyCode.Return || code != KeyCode.Backspace)
        {
            GetComponentInChildren<Text>().text = $"{(char)code}";
            return;
        }

        switch(code)
        {
            case KeyCode.Backspace:
                GetComponentInChildren<Text>().text = "<-";
                break;
        }
    }
}
