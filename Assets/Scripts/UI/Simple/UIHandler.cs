using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : Singleton<UIHandler>
{
    [SerializeField] UIDragableElement currentDragElement;
    [SerializeField] Transform currentDragParent;
    [SerializeField] Vector3 currentDragPosition;
    internal void StartDrag(UIDragableElement uIDragableElement)
    {
        currentDragParent = uIDragableElement.transform.parent;
        currentDragPosition = uIDragableElement.transform.position;
        Vector3 scale = uIDragableElement.transform.localScale;
        uIDragableElement.transform.SetParent(transform, worldPositionStays: false);
        uIDragableElement.transform.localScale = scale;
    }

    internal void EndDrag(UIDragableElement uIDragableElement)
    {
        Debug.LogWarning("End Drag...");

        uIDragableElement.transform.SetParent(currentDragParent, worldPositionStays: false);
        uIDragableElement.transform.position = currentDragPosition;
    }
}
