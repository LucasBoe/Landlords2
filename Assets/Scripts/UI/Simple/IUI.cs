using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUI {

}

public interface IUIFloat : IUI
{
    float GetUIFloat();
}

public interface IUIString : IUI
{
    string GetUIString();
}

public interface IUISprite : IUI
{
    Sprite GetUISprite();
    Color GetUIImageColor();
}
