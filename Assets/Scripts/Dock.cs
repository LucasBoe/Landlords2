using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    [SerializeField] UIUseableEntityList dockListUI;
    [SerializeField] SubscribableList<UseableEntity> dockList = new SubscribableList<UseableEntity>();
    [SerializeField] UseableEntityData useableEntityTestDataTEMP;

    private void Start()
    {
        dockListUI.Init(dockList);
        dockList.Add(new UseableEntity(useableEntityTestDataTEMP));
    }

    [Button]
    private void TestTEMP()
    {
        dockList.Add(new UseableEntity(useableEntityTestDataTEMP));
    }
}
