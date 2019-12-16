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

    public void Focus()
    {
        _keyboard = keyboardCanvas.GetComponentInChildren<VRKeyboard>();
        if (_keyboard)
        {
            _keyboard.keyEvent = onKeyEvent;
            return;
        }

        _keyboard = Instantiate(keyboardPrefab, keyboardCanvas.transform);
        _keyboard.keyEvent = onKeyEvent;
    }

    public class KeyEvent : UnityEvent<KeyCode> {}
}
