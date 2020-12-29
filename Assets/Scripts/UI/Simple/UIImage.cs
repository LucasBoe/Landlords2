using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImage : Image, IUIConnectable
{
    object connected;
    [SerializeField] Color emptyColor;
    [SerializeField] Sprite emptySprite;

    public IUISprite GetConnected<IUISprite>()
    {
        return (IUISprite)connected;
    }

    public void HideUI()
    {
        if (this == null)
            return;

        Destroy(gameObject);
    }

    public void Init<T>(T obj)
    {
        connected = obj;
    }

    public void UpdateUI()
    {
        bool isNull = connected as IUISprite == null;

        color = isNull ? emptyColor : (connected as IUISprite).GetUIImageColor();
        sprite = isNull ? emptySprite : (connected as IUISprite).GetUISprite();
    }
}
