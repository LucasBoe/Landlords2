using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : Singleton<Dock>
{
    [SerializeField] SubscribableList<UseableEntity> dockList = new SubscribableList<UseableEntity>();


    protected override void Start()
    {
        base.Start();

        Game.UIHandler.CreateUI(this);
    }

    public SubscribableList<UseableEntity> GetUseableEntityList()
    {
        return dockList;
    }

    internal void Add(UseableEntity[] useableEntities)
    {
        dockList.Add(useableEntities);
    }

    public float GetFilledFloat()
    {
        return (float)dockList.Count / 20f;
    }
}
