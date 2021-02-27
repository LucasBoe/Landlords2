using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : MonoBehaviour
{
    [Expandable]
    [SerializeField] List<ProcessData> possibleProcesses = new List<ProcessData>();
    [SerializeField] public SubscribableList<Process> ActiveProcesses = new SubscribableList<Process>();

    private void Start()
    {
        ActivateAll();
        Game.UIHandler.CreateUI<UIProcessorDot>(this);
    }

    public ProcessData[] GetProcessDatas()
    {
        return possibleProcesses.ToArray();
    }

    public Process GetProcess(int index)
    {
        if (possibleProcesses != null && possibleProcesses.Count > 0)
            return GetProcess(possibleProcesses[index]);

        return null;
    }

    internal void DeactivateProcess(ProcessData dataTEMP)
    {
        Process process = GetActiveProcessByDataIfActive(dataTEMP);
        if (process != null)
            ActiveProcesses.Remove(process);
    }

    public Process GetProcess(ProcessData processData)
    {
        Process process = GetActiveProcessByDataIfActive(processData);

        if (process == null)
            process = ActivateProcess(processData);

        return process;
    }

    public Process GetActiveProcessByDataIfActive(ProcessData processData)
    {
        foreach (Process item in ActiveProcesses)
        {
            if (item.Data == processData)
                return item;
        }

        return null;
    }

    public void ActivateAll() {
        foreach (ProcessData data in possibleProcesses)
            if (GetActiveProcessByDataIfActive(data) == null)
                ActivateProcess(data);
    }

    public Process ActivateProcess(ProcessData processData) {
        Process newProcess = new Process(processData);
        ActiveProcesses.AddUnique(newProcess);
        return newProcess;
    }
}
