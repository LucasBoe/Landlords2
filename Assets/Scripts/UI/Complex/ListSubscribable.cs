using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ListSubscribable<T>: IList
{
    public Action<int> ChangeRemoveAt;
    public Action<int> ChangeAddAt;
    public Action<int> ChangeAt;
    public Action<T> ChangeRemove;
    public Action<T> Change;
    public Action<T> ChangeAdd;

    public int MaxCount = -1;

    //implementation of the IList interface using T
    [SerializeField] List<T> list = new List<T>();

    public bool IsFixedSize => false;
    public bool IsReadOnly => false;

    public int Count => list.Count;

    public bool IsSynchronized => false;

    public object SyncRoot => null;

    public object this[int index] { get => list[index]; set { list[index] = (T)value; } }

    public int Add(object value)
    {
        list.Add((T)value);

        ChangeAdd?.Invoke((T)value);
        ChangeAddAt?.Invoke(list.IndexOf((T)value));

        return list.IndexOf((T)value);
    }

    public void Clear()
    {
        list.Clear();
    }

    public bool Contains(object value)
    {
        return list.Contains((T)value);
    }

    public int IndexOf(object value)
    {
        return list.IndexOf((T)value);
    }

    public void Insert(int index, object value)
    {
        list.Insert(index, (T)value);

        ChangeAdd?.Invoke((T)value);
        ChangeAddAt?.Invoke(index);
    }

    public void Remove(object value)
    {
        int index = list.IndexOf((T)value);
        list.Remove((T)value);
        ChangeRemove?.Invoke((T)value);
        ChangeRemoveAt?.Invoke(index);
    }

    public void RemoveAt(int index)
    {
        T obj = list[index];
        list.RemoveAt(index);
        ChangeRemove?.Invoke(obj);
        ChangeRemoveAt?.Invoke(index);
    }

    public void Changed(int index) {
        Change?.Invoke(list[index]);
        ChangeAt?.Invoke(index);
    }

    public void Changed(object value) {
        Change?.Invoke((T)value);
        ChangeAt?.Invoke(list.IndexOf((T)value));
    }

    public void CopyTo(Array array, int index)
    {
        list.CopyTo(array as T[], index);
    }

    public IEnumerator GetEnumerator()
    {
        return list.GetEnumerator();
    }

    public void ClickedOn(int index)
    {
        Debug.Log("clicked on " + index);
    }
}
