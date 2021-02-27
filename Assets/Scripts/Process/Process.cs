using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIProgress
{
    event Action OnStartProgressEvent;
    event Action OnFinishProgressEvent;

    float GetStartTime();
    float GetDuration();

    bool IsInProgress { get; }
}


[System.Serializable]
public class Process : IUIString, IUIProgress
{
    [Expandable]
    [SerializeField]
    public ProcessData Data;
    public ProcessStatus status = ProcessStatus.NotRunning;
    public bool IsInProgress => status == ProcessStatus.Running;
    float startTime = float.MinValue;
    float duration = float.MaxValue;
    public bool Loop;

    public SubscribableList<UseableEntity> inputEntities = new SubscribableList<UseableEntity>();
    public SubscribableList<UseableEntity> nonHumanEntities = new SubscribableList<UseableEntity>();


    public event Action OnStartProgressEvent;
    public event Action OnFinishProgressEvent;

    public Process (ProcessData data)
    {
        Data = data;
        inputEntities.MaxCount = 3;
    }

    public virtual bool CanGetStarted ()
    {
        return status != ProcessStatus.Running && CheckInputConditions();
    }

    public void TryStart(Processor processor)
    {
        if (CanGetStarted())
            processor.StartCoroutine(ProcessRoutine());
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
            {
                return false;
            }
        }

        return true;
    }

    public virtual IEnumerator ProcessRoutine ()
    {
        bool first = true;
        status = ProcessStatus.Running;
        inputEntities.SetLocked(true);

        while (first || Loop)
        {
            first = false;
            startTime = Time.time;
            duration = CalculateDuration();
            OnStartProgressEvent?.Invoke();
            yield return new WaitForSeconds(duration);
            nonHumanEntities.Add(Result());
            OnFinishProgressEvent?.Invoke();
        }

        status = ProcessStatus.NotRunning;
        inputEntities.SetLocked(false);
    }

    private void StartProcess()
    {

    }

    internal void SetLoop(bool shouldLoop)
    {
        Loop = shouldLoop;
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

    public float GetStartTime()
    {
        return startTime;
    }

    public float GetDuration()
    {
        return duration;
    }

    public enum ProcessStatus
    {
        Running,
        NotRunning
    }
}
