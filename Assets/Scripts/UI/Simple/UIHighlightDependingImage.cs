using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighlightDependingImage : Image, IUIHighlightable
{
    object connected;

    public IUISprite GetConnected<IUISprite>()
    {
        return (IUISprite)connected;
    }

    public void HideUI()
    {
        //
    }

    public void Init<T>(T obj)
    {
        //
    }

    public void SetSelected(bool isSelected) {

        //if selected reset the color through update, else make it transparent
        if (isSelected)
            color = new Color(color.r, color.g, color.b, 0);
        else
            color = new Color(color.r, color.g, color.b, 1);
    }

    public void UpdateUI<T>(T obj)
    {
        connected = obj;
    }
}
