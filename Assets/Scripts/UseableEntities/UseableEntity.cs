using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
















    public static UseableEntity[] CreateArrayFromDataArray(List<UseableEntityData> spawnOnStart)
    {
        List<UseableEntity> useableEntities = new List<UseableEntity>();

        foreach (UseableEntityData data in spawnOnStart)
        {
            useableEntities.Add(new UseableEntity(data));
        }

        return useableEntities.ToArray();
    }
}
