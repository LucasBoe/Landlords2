using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ProcessData : ScriptableObject
{
    public ProcessInputData[] input;
    public ProcessOutputData[] output;
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
}
