using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : Singleton<UIHandler>
{
    [SerializeField] public Canvas UICanvas, InWorldCanvas;

    [SerializeField] UIProcessorBase uIProcessorDotPrefab;
    [SerializeField] UIProcessorBase uIProcessorPrefab;

    [SerializeField] UIUseableEntityList uIdockPrefab; 

    [ShowNonSerializedField] private UIDragableElement currentDragElement;
    [ShowNonSerializedField] private Transform currentDragParent;



    [ShowNonSerializedField] private Vector3 currentDragPosition;
    internal void StartDrag(UIDragableElement uIDragableElement)
    {
        currentDragParent = uIDragableElement.transform.parent;
        currentDragPosition = uIDragableElement.transform.position;
        Vector3 scale = uIDragableElement.transform.localScale;
        uIDragableElement.transform.SetParent(UICanvas.transform, worldPositionStays: false);
        uIDragableElement.transform.localScale = scale;
    }

    internal void EndDrag(UIDragableElement uIDragableElement)
    {
        Debug.LogWarning("End Drag...");

        uIDragableElement.transform.SetParent(currentDragParent, worldPositionStays: false);
        uIDragableElement.transform.position = currentDragPosition;
    }

    public void CreateUI<T>(Processor processor)
    {
        if (typeof(T) == typeof(UIProcessorDot))
            CreateUIInstance(processor, uIProcessorDotPrefab);
        else if (typeof(T) == typeof(UIProcessor))
            CreateUIInstance(processor, uIProcessorPrefab);
    }


    public void CreateUI(Dock dock)
    {
        UIUseableEntityList instance = Instantiate(uIdockPrefab, UICanvas.transform);
        instance.Init(dock.GetUseableEntityList());
    }



    private void CreateUIInstance(Processor processor, UIProcessorBase prefab)
    {
        Debug.LogWarning("Instatiate: " + prefab.name);
        Debug.LogWarning("Connect: " + processor.name);

        UIProcessorBase instance = Instantiate(prefab, processor.transform.position, Quaternion.identity, InWorldCanvas.transform);
        instance.Init(processor);
    }
}