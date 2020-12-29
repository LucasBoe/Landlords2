using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class UIProcessor : UIBehaviour
{
    int foregroundProcessIndex = 0;

    [SerializeField] Processor processor;

    [SerializeField] UIProcessList uIHeaderProcesses;
    [SerializeField] UIUseableEntityList inputListUI;
    [SerializeField] UIUseableEntityList nonHumanListUI;

    [SerializeField] UseableEntityData useableEntityDataTEMP;

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
        uIHeaderProcesses.DefineOnClick(ClickedOn);
        ChangeSelectedProcess(0); //this connects the ui with the first availiable process

        //TEMP
        p.GetProcess(0).nonHumanEntities.Add(new UseableEntity(useableEntityDataTEMP));
    }

    private void ClickedOn(int index) {
        ChangeSelectedProcess(index);
    }

    public void ChangeSelectedProcess(int foregroundIndex) {

        Process process = GetConnected<Processor>().GetProcess(foregroundIndex);

        inputListUI.Init(process.inputEntities);
        nonHumanListUI.Init(process.nonHumanEntities);

        uIHeaderProcesses.HighlightElement = foregroundIndex;
    }
}
