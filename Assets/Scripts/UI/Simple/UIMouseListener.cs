using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseListener : EventTrigger
{
    public event System.Action OnMouseDown;
    public event System.Action OnMouseUp;
    public event System.Action OnMouseEnter;
    public event System.Action OnMouseExit;

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnMouseDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        OnMouseUp?.Invoke();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter?.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit?.Invoke();
    }
}
