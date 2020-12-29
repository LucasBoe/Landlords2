using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : Button, IUIClickable
{
    object connected;
    System.Action OnClick;

    public T GetConnected<T>()
    {
        return (T)connected;
    }

    public void HideUI()
    {
        if (this == null)
            return;

        Destroy(gameObject);
    }

    public void Init<T>(T obj)
    {
        Debug.Log("init button");
        connected = obj;
    }


    public void SetClickMethod(Action clickMethod)
    {
        OnClick = clickMethod;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    public void UpdateUI()
    {
        //
    }
}
