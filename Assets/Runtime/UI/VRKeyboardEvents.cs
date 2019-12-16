using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VRKeyboardEvents : UIBehaviour
{
    public Canvas keyboardCanvas;
    public VRKeyboard keyboardPrefab;

    public KeyEvent onKeyEvent = new KeyEvent();

    private VRKeyboard _keyboard;

    public void Open(BaseEventData data)
    {
        _keyboard = keyboardCanvas.GetComponentInChildren<VRKeyboard>();
        if (_keyboard)
            return;

        _keyboard = Instantiate(keyboardPrefab, keyboardCanvas.transform);
        _keyboard.keyEvent = onKeyEvent;
    }

    public void Close(BaseEventData data)
    {
        if (!_keyboard)
            return;

        PointerEventData pointerEvent = data as PointerEventData;
        if (pointerEvent != null && pointerEvent.pointerEnter != null)
        {
            if (pointerEvent.pointerEnter.GetComponent<VRKeyboard>() != null
                || pointerEvent.pointerEnter.GetComponentInParent<VRKeyboard>() != null)
            {
                return;
            }
        }

        Destroy(_keyboard.gameObject);
        _keyboard = null;
    }

    public class KeyEvent : UnityEvent<KeyCode> {}
}
