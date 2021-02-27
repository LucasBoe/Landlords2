using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIProcessorDot : UIProcessorBase
{
    Process runningProcess;

    public override void Init<T>(T obj)
    {
        base.Init(obj);
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        Process p = GetRunningProcess();

        if (p != runningProcess)
        {
            runningProcess = p;
            if (runningProcess != null)
                progressBarUI.Init(runningProcess);
            else
                progressBarUI.Disconnect();
        }
    }

    private Process GetRunningProcess()
    {
        foreach (Process process in GetConnected<Processor>().ActiveProcesses)
        {
            if (process.IsInProgress)
                return process;
        }

        return null;
    }
}
