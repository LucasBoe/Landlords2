using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableEntity
{
    string type;
    int amount;

    public UseableEntityData GetData() {
        return UseableEntityDataHandler.GetInstance().GetData(type);
    }
}
