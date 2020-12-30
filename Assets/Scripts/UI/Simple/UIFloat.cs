using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFloat : MonoBehaviour, IUIConnectable
{
    object connected;
    TMP_Text text;

    public IUIFloat GetConnected<IUIFloat>()
    {
        return (IUIFloat)connected;
    }

    public void HideUI()
    {
        text = GetComponent<TMP_Text>();
    }

    public void Init<T>(T obj)
    {
        connected = obj;
        text = GetComponent<TMP_Text>();
        if (text == null)
            text = gameObject.AddComponent<TMP_Text>();
    }

    public void UpdateUI<T>(T obj)
    {
        connected = obj;
        text.text = GetConnected<IUIFloat>().GetUIFloat().ToString();
    }
}
