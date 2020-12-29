using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UseableEntity : IUISprite, IUIString
{
    string type;
    int amount;
    [SerializeField] private UseableEntityData data;

    public UseableEntity(UseableEntityData data)
    {
        this.data = data;
    }

    public UseableEntityData GetData() {
        return Game.UseableEntityDataHandler.GetData(type);
    }

    public Color GetUIImageColor()
    {
        if (data != null)
            return data.color;

        return Color.black;

    }

    public Sprite GetUISprite()
    {
        return null;
    }

    public string GetUIString()
    {
        if (data != null)
            return data.DisplayName;

        return "";
    }
}
