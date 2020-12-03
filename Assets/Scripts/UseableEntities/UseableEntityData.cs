using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UseableEntityData : ScriptableObject
{
    public string type;
    public string DisplayName;
    public bool isStackable;
    public bool isConsumeable;

    public Color color;
}
