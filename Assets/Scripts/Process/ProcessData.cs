using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
[System.Serializable]
public class ProcessData : ScriptableObject
{
    public ProcessInputData[] input;
    public ProcessOutputData[] output;
    public float Duration;

    public UseableEntity[] GetToRemove(SubscribableList<UseableEntity> inputEntities)
    {
        List<string> inputToRemove = new List<string>();
        foreach (var item in input.Where(d => !d.allwaysOutput))
            inputToRemove.Add(item.entity.type);

        return inputEntities.ToArray().Where(i => inputToRemove.Contains(i.GetData().type)).ToArray();
    }
}

[System.Serializable]
public struct ProcessInputData
{
    public UseableEntityData entity;
    public int amountMin;
    public int amountMax;
    public bool allwaysOutput;
}


[System.Serializable]
public struct ProcessOutputData {
    public UseableEntityData entity;
    public int baseAmount;
    public int amountPerInput;
    public float probability;
    public bool outputToDock;
}
