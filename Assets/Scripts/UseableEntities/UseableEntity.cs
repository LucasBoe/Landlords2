using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UseableEntity : IUISprite, IUIString
{
    [SerializeField] string type;
    [SerializeField] int amount;
    [SerializeField] private UseableEntityData data;

    public UseableEntity(UseableEntityData data)
    {
        this.data = data;
        type = data.type;
    }

    public UseableEntityData GetData() {

        if (data == null)
            data = Game.UseableEntityDataHandler.GetData(type);

        return data;
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
