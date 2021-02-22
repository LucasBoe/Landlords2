using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragableElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rect;
    Graphic[] raycastTargets;
    protected bool isInDrag = false;


    private void Start ()
    {
        rect = GetComponent<RectTransform>();
        Graphic[] graphics = GetComponentsInChildren<Graphic>();
        raycastTargets = graphics.Where(g => g.raycastTarget).ToArray();
    }

    protected virtual bool IsDragabled()
    {
        return true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsDragabled())
        {
            isInDrag = true;
            Debug.Log("Beign Drag");
            SetRayCastable(false);
            Game.UIHandler.StartDrag(this);
        }


    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isInDrag)
            rect.position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (isInDrag)
        {
            isInDrag = false;
            Debug.Log("End Drag " + (eventData.pointerCurrentRaycast.gameObject != null ? (" ( " + eventData.pointerCurrentRaycast.gameObject.name + " )") : " ( / )"));
            SetRayCastable(true);
            Game.UIHandler.EndDrag(this);
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //
    }
    private void SetRayCastable(bool isTarget)
    {
        Array.ForEach(raycastTargets, graphic => SetRayCastable(graphic, isTarget));
    }

    private void SetRayCastable(Graphic graphic, bool target)
    {
        graphic.raycastTarget = target;
    }
}
