﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropReceiver<T>
{
    public bool IsSameType<T1>(T1 type);
    public bool IsDifferentList(SubscribableList<T> list);
    public bool WouldRecieve(T source);
    public void Receive(T source);
}

public class UIList<T> : MonoBehaviour, IDropReceiver<T>
{
    public GameObject elementPrefab;
    [SerializeField] SubscribableList<T> connectedList = new SubscribableList<T>();
    protected List<UIListElement<T>> elementRepresentations = new List<UIListElement<T>>();

    System.Action<int> OnClick;

    /// <summary>
    /// Hides the user interface by destroying the gameObject after unsubscribing from the change events.
    /// </summary>
    public virtual void HideUI()
    {
        connectedList.Change -= OnChange;
        connectedList.ChangeAdd -= OnChangeAdd;
        connectedList.ChangeRemove -= OnChangeRemove;

        Destroy(gameObject);
    }

    /// <summary>
    /// Initialize the visualization by saving the list and subscribing to change events.
    /// </summary>
    /// <param name="obj">the object to connect with.</param>
    public virtual void Init(SubscribableList<T> obj)
    {
        //hide visualized elements that were there before
        HideAll();

        connectedList = obj;

        connectedList.Change += OnChange;
        connectedList.ChangeAdd += OnChangeAdd;
        connectedList.ChangeRemove += OnChangeRemove;

        foreach (T item in connectedList)
            OnChangeAdd(item);

    }

    public void DefineOnClick(System.Action<int> clickedOn)
    {
        OnClick = clickedOn;
    }

    protected virtual void OnChange(T element)
    {
        foreach (UIListElement<T> item in elementRepresentations)
        {
            if (item.Source.GetHashCode() == element.GetHashCode())
                item.UpdateUI();
        }
    }

    protected virtual void OnChangeAdd(T element)
    {
        Debug.LogWarning("ChangeAdd " + element);

        elementRepresentations.Add(InstantiateAndConnectNewElement(element, ClickedOn));
    }

    protected virtual void OnChangeRemove(T element)
    {
        foreach (UIListElement<T> item in elementRepresentations)
        {
            if (item.Source.GetHashCode() == element.GetHashCode())
                item.HideUI();

            //Debug.Log(item.Source.GetHashCode() + " compare with: " + element.GetHashCode() + " : " + (item.Source.GetHashCode() == element.GetHashCode()));
        }
    }


    private void ClickedOn(UIListElement<T> representation)
    {
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

    private UIListElement<T> InstantiateAndConnectNewElement(T connectedElement, System.Action<UIListElement<T>> clickedOn)
    {
        Debug.Log("Am I even existing? " + this);

        GameObject instance = Instantiate(elementPrefab, transform);
        UIListElement<T> element = instance.GetComponent<UIListElement<T>>();
        if (element == null)
        {
            Debug.LogError("No element found, please add fitting script to prefab");
        }

        element.InitToList(connectedList, connectedElement, clickedOn, instance.GetComponentsInChildren<IUIConnectable>());
        return element;
    }

    private void HideAll()
    {
        //unsubscribe from any previous list
        connectedList.Change -= OnChange;
        connectedList.ChangeAdd -= OnChangeAdd;
        connectedList.ChangeRemove -= OnChangeRemove;

        if (elementRepresentations == null)
            return;

        foreach (UIListElement<T> item in elementRepresentations)
            item.HideUI();
    }

    public virtual bool IsSameType<T1>(T1 type)
    {
        return (typeof(T) == typeof(T1));
    }

    public virtual bool WouldRecieve(T source)
    {
        return connectedList.WouldReceive(source);
    }

    public void Receive(T source)
    {
        connectedList.Add(source as object);
    }

    public bool IsDifferentList(SubscribableList<T> list)
    {
        return connectedList != list;
    }
}