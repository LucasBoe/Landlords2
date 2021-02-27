using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class UIProcessor : UIBehaviour
{
    int foregroundProcessIndex = 0;

    [SerializeField] Processor processor;

    [SerializeField] UIProcessList uIHeaderProcesses;
    [SerializeField] UIUseableEntityList inputListUI;
    [SerializeField] UIUseableEntityList nonHumanListUI;
    [SerializeField] UIProgressBar progressBarUI;

    [SerializeField] Button startButton;
    [SerializeField] Toggle loopToggle;

    Process foregroundProcess;

    private void Start()
    {
        Init(processor); //TEMP
    }

    public override void Init<T>(T obj)
    {
        base.Init(obj);

        Processor p = GetConnected<Processor>();

        p.ActivateAll(); //this should be done in the processor itself at some point
        uIHeaderProcesses.Init(p.ActiveProcesses); //all ui elements need to get connected through init
        uIHeaderProcesses.DefineOnClick(ClickOnHeaderItem);
        ChangeSelectedProcess(0); //this connects the ui with the first availiable process
    }

    private void ClickOnHeaderItem(int index)
    {
        ChangeSelectedProcess(index);
    }

    public void ChangeSelectedProcess(int processIndex)
    {
        if (foregroundProcess != null)
        {
            foregroundProcess.inputEntities.ChangeAny -= UpdateUI;
            foregroundProcess.nonHumanEntities.ChangeAny -= UpdateUI;
            foregroundProcess.OnFinishProgressEvent -= UpdateUI;
        }


        foregroundProcess = GetConnected<Processor>().GetProcess(processIndex);

        SubscribableList<UseableEntity> input = foregroundProcess.inputEntities;
        SubscribableList<UseableEntity> output = foregroundProcess.nonHumanEntities;

        inputListUI.Init(input);
        nonHumanListUI.Init(output);

        input.ChangeAny += UpdateUI;
        output.ChangeAny += UpdateUI;
        foregroundProcess.OnFinishProgressEvent += UpdateUI;

        progressBarUI.Init(foregroundProcess);

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(TryStartCurrentProcess);

        loopToggle.onValueChanged.RemoveAllListeners();
        loopToggle.onValueChanged.AddListener(foregroundProcess.SetLoop);

        uIHeaderProcesses.HighlightElement = processIndex;
        UpdateUI();
    }
    private void UpdateUI()
    {
        startButton.interactable = foregroundProcess.CanGetStarted();
        loopToggle.isOn = foregroundProcess.Loop;
    }

    private void TryStartCurrentProcess()
    {
        foregroundProcess.TryStart(GetConnected<Processor>());
    }
}
