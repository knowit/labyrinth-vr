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
            ProcessEvent(new Event
            {
                character = (char)key,
                keyCode = key,
                type = EventType.KeyDown
            });

            ForceLabelUpdate();
        }));
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        GetComponentInChildren<VRKeyboardEvents>().Close(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        GetComponentInChildren<VRKeyboardEvents>().Close(eventData);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        GetComponentInChildren<VRKeyboardEvents>().Open(eventData);
    }
}
