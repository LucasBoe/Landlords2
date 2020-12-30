using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIList<T> : MonoBehaviour
{
    public bool HandleAsArray;
    public GameObject elementPrefab;
    ListSubscribable<T> connectedList = new ListSubscribable<T>();
    protected List<UIListElement<T>> elementRepresentations = new List<UIListElement<T>>();

    System.Action<int> OnClick;

    /// <summary>
    /// Hides the user interface by destroying the gameObject after unsubscribing from the change events.
    /// </summary>
    public virtual void HideUI()
    {
        connectedList.Change += OnChange;
        connectedList.ChangeAt += OnChangeAt;

        connectedList.ChangeAdd += OnChangeAdd;
        connectedList.ChangeAddAt += OnChangeAddAt;

        connectedList.ChangeRemove += OnChangeRemove;
        connectedList.ChangeRemoveAt += OnChangeRemoveAt;

        Destroy(gameObject);
    }

    /// <summary>
    /// Initialize the visualization by saving the list and subscribing to change events.
    /// </summary>
    /// <param name="obj">the object to connect with.</param>
    public virtual void Init(ListSubscribable<T> obj)
    {
        //hide visualized elements that were there before
        HideAll();

        connectedList = obj;
        HandleAsArray = (connectedList.MaxCount != -1);

        connectedList.Change += OnChange;
        connectedList.ChangeAt += OnChangeAt;

        connectedList.ChangeAdd += OnChangeAdd;
        connectedList.ChangeAddAt += OnChangeAddAt;

        connectedList.ChangeRemove += OnChangeRemove;
        connectedList.ChangeRemoveAt += OnChangeRemoveAt;

        if (HandleAsArray)
        {
            Debug.Log("Init Array UI List");

            for (int i = 0; i < connectedList.MaxCount; i++)
            {
                T source = (T)((connectedList != null && connectedList.Count > i) ? connectedList[i] : null);
                elementRepresentations.Add(new UIListElement<T>(source, ClickedOn, InstantiateAndReturnConnectables()));
            }
        }
        else
        {
            foreach (T item in connectedList)
                OnChangeAdd(item);
        }

    }

    public void DefineOnClick(System.Action<int> clickedOn)
    {
        OnClick = clickedOn;
    }

    protected virtual void OnChange(T element)
    {
        if (!HandleAsArray)
        {
            foreach (UIListElement<T> item in elementRepresentations)
            {
                if (item.Source.GetHashCode() == element.GetHashCode())
                    item.UpdateUI();
            }
        }
    }

    protected virtual void OnChangeAt(int index)
    {
        if (HandleAsArray)
            elementRepresentations[index].UpdateUI();
    }

    protected virtual void OnChangeAdd(T element)
    {
        if (!HandleAsArray)
            elementRepresentations.Add(new UIListElement<T>(element, ClickedOn, InstantiateAndReturnConnectables()));
    }

    protected virtual void OnChangeAddAt(int index)
    {
        if (HandleAsArray)
            elementRepresentations[index] = new UIListElement<T>((T)connectedList[index], ClickedOn, InstantiateAndReturnConnectables());
    }

    protected virtual void OnChangeRemove(T element)
    {
        if (!HandleAsArray)
        {
            foreach (UIListElement<T> item in elementRepresentations)
            {
                if (item.Source.GetHashCode() == element.GetHashCode())
                    item.HideUI();

                //Debug.Log(item.Source.GetHashCode() + " compare with: " + element.GetHashCode() + " : " + (item.Source.GetHashCode() == element.GetHashCode()));
            }
        }
    }

    protected virtual void OnChangeRemoveAt(int index)
    {
        if (HandleAsArray)
        {
            elementRepresentations[index].HideUI();
            elementRepresentations.RemoveAt(index);
        }
    }

    private void ClickedOn(UIListElement<T> representation) {
        for (int i = 0; i < elementRepresentations.Count; i++)
        {
            if (elementRepresentations[i] != null && elementRepresentations[i] == representation)
            {
                OnClick?.Invoke(i);
                //connectedList.ClickedOn(i);
                return;
            }
        }
    }

    private IUIConnectable[] InstantiateAndReturnConnectables()
    {
        GameObject instance = Instantiate(elementPrefab, transform);
        return instance.GetComponentsInChildren<IUIConnectable>();
    }

    private void HideAll() {

        if (elementRepresentations == null)
            return;

        foreach (UIListElement<T> item in elementRepresentations)
            item.HideUI();
    }
}

public class UIListElement<T>
{
    List<IUIConnectable> connectables = new List<IUIConnectable>();
    public T Source;
    System.Action<UIListElement<T>> OnClick;

    public UIListElement (T source, System.Action<UIListElement<T>> clickAction, params IUIConnectable[] connectables)
    {
        Debug.Log("create new element with " + connectables.Length + " connectables.");

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

    private void ClickedOn() {
        OnClick?.Invoke(this);
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