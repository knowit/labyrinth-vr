using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(VRKeyboardEvents))]
public class VRInputField : InputField
{
    protected override void Start()
    {
        base.Start();

        var keyboard = GetComponentInChildren<VRKeyboardEvents>();
        keyboard.onKeyEvent.AddListener(new UnityAction<KeyCode>(key =>
        {
            Debug.Log($"VRKeyboard - {key}");

            if (key == KeyCode.Backspace && text.Length > 0)
            {
                text = text.Remove(text.Length-1,1);   
            }
            if (key == KeyCode.Return)
            {
                onEndEdit.Invoke(text);
            }
            else
            {
                text += (char)key;
            }

        }));
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        GetComponentInChildren<VRKeyboardEvents>().Focus();
    }
}
