using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VRKeyboard : UIBehaviour
{
    public VRKeyboardKey keyPrefab;
    public VRKeyboardEvents.KeyEvent keyEvent;

    private List<KeyCode> _keys = new List<KeyCode>
    {
        KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2,
        KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8,
        KeyCode.Alpha9,

        KeyCode.Period, KeyCode.Backspace
    };

    protected override void Start()
    {
        _keys.ForEach(key =>
        {
            var kkey = Instantiate(keyPrefab, transform);
            kkey.SetKey(key);
            kkey.onClick.AddListener(new UnityAction(() =>
            {
                keyEvent.Invoke(key);
            }));
        });
    }
}
