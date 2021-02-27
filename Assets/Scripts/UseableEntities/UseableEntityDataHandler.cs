using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableEntityDataHandler : Singleton<UseableEntityDataHandler>
{
    UseableEntityData[] datas;

    public UseableEntityData GetData(string type)
    {
        if (datas == null || datas.Length == 0)
            datas = FetchDatasFromResources();

        foreach (var data in datas)
        {
            if (data.type == type)
                return data;
        }

        Debug.LogError("Info of " + type + " could not be found");
        return new UseableEntityData();
    }

    private UseableEntityData[] FetchDatasFromResources()
    {
        return Resources.LoadAll<UseableEntityData>("UseableEntities");
    }
}
