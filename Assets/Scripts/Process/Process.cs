using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Process : IUIString
{
    [Expandable]
    [SerializeField]
    public ProcessData Data;
    public ProcessStatus status = ProcessStatus.Running;
    public bool Loop;

    public SubscribableList<UseableEntity> inputEntities = new SubscribableList<UseableEntity>();
    public SubscribableList<UseableEntity> nonHumanEntities = new SubscribableList<UseableEntity>();

    System.Action OnStartProcessing;
    System.Action OnFinishProcessing;

    public Process (ProcessData data)
    {
        Data = data;
        inputEntities.MaxCount = 3;
    }

    public virtual bool CanGetStarted ()
    {
        return status == ProcessStatus.Running && CheckInputConditions();
    }

    private bool CheckInputConditions()
    {
        Dictionary<UseableEntityData, int> inputAmountPairs = new Dictionary<UseableEntityData, int>();
        foreach (UseableEntity entity in inputEntities) {
            UseableEntityData data = entity.GetData();

            if (inputAmountPairs.ContainsKey(data))
                inputAmountPairs[data] += 1;
            else
                inputAmountPairs.Add(data, 1);
        }

        //if any input item missing or the amount smaller than needed : return false
        foreach (ProcessInputData inputItem in Data.input)
        {
            if (!inputAmountPairs.ContainsKey(inputItem.entity) || inputAmountPairs[inputItem.entity] < inputItem.amountMin)
                return false;
        }

        return true;
    }

    public virtual IEnumerator ProcessRoutine ()
    {
        status = ProcessStatus.Running;
        OnStartProcessing?.Invoke();
        yield return new WaitForSeconds(CalculateDuration());
        OnFinishProcessing?.Invoke();
    }

    public virtual UseableEntity[] Result()
    {
        List<UseableEntity> results = new List<UseableEntity>();

        foreach (var item in Data.output)
        {
            if (UnityEngine.Random.value <= item.probability)
            {
                for (int i = 0; i < (item.baseAmount + item.amountPerInput * inputEntities.Count); i++)
                {
                    results.Add(new UseableEntity(item.entity));
                }
            }
        }

        return results.ToArray();
    }

    public virtual float CalculateDuration()
    {
        return 10f;
    }

    public string GetUIString()
    {
        return Data.name;
    }

    public enum ProcessStatus
    {
        Running,
        NotRunning
    }
}
