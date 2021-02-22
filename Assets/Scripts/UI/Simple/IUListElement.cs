using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIListElement<T> : UIDragableElement
{
    List<IUIConnectable> connectables = new List<IUIConnectable>();
    SubscribableList<T> List;
    public T Source;
    System.Action<UIListElement<T>> OnClick;

    public void InitToList(SubscribableList<T> list, T source, System.Action<UIListElement<T>> clickAction, params IUIConnectable[] connectables)
    {
        Debug.Log("create new element with " + connectables.Length + " connectables.");

        List = list;
        OnClick = clickAction;

        foreach (IUIConnectable connectable in connectables)
        {
            connectable.UpdateUI(source);

            if (connectable as IUIClickable != null)
                (connectable as IUIClickable).SetClickMethod(ClickedOn);

            this.connectables.Add(connectable);
        }

        Source = source;
    }

    public void HideUI()
    {
        foreach (IUIConnectable connectable in connectables)
        {
            connectable.HideUI();
        }
    }

    private void ClickedOn()
    {
        OnClick?.Invoke(this);
    }

    protected override bool IsDragabled()
    {
        return Source != null;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!isInDrag)
            return;

        base.OnEndDrag(eventData);
        if (TryDrop(eventData.pointerCurrentRaycast.gameObject))
            List.Remove(Source);
    }

    protected virtual bool TryDrop(GameObject potentialDropTarget)
    {
        if (potentialDropTarget == null)
            return false;

        IDropReceiver<T> dropReceiver = potentialDropTarget.GetComponentInParent<IDropReceiver<T>>();
        if (dropReceiver != null
            && dropReceiver.IsSameType(Source)
            && dropReceiver.IsDifferentList(List)
            && dropReceiver.WouldRecieve(Source))
        {
            dropReceiver.Receive(Source);
            return true;
        }

        return false;
    }

    public void SetSelected(bool isSelected)
    {
        Debug.Log("is selected: " + isSelected);

        foreach (IUIConnectable connectable in connectables)
        {
            if (connectable as IUIHighlightable != null)
                (connectable as IUIHighlightable).SetSelected(isSelected);
        }
    }

    public void UpdateUI()
    {
        foreach (IUIConnectable connectable in connectables)
        {
            connectable.UpdateUI(Source);
        }
    }
}
