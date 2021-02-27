using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SubscribableList<T>: IList
{
    public event Action<T> ChangeRemove;
    public event Action<T> Change;
    public event Action<T> ChangeAdd;
    public event Action ChangeAny;

    private bool isLocked;
    public int MaxCount = -1;

    //implementation of the IList interface using T
    [SerializeField] List<T> list = new List<T>();

    public bool IsFixedSize => false;
    public bool IsReadOnly => false;

    public bool IsLocked { get => isLocked; }

    public int Count => list.Count;

    public bool IsSynchronized => false;

    public object SyncRoot => null;

    public object this[int index] { get => list[index]; set { list[index] = (T)value; } }

    public void Add(object[] array)
    {
        foreach (var value in array)
        {
            list.Add((T)value);

            ChangeAdd?.Invoke((T)value);
        }
        ChangeAny?.Invoke();
    }

    public int Add(object value)
    {
        list.Add((T)value);

        ChangeAdd?.Invoke((T)value);
        ChangeAny?.Invoke();

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
        ChangeAny?.Invoke();
    }

    public void Remove(object value)
    {
        int index = list.IndexOf((T)value);
        list.Remove((T)value);
        ChangeRemove?.Invoke((T)value);
        ChangeAny?.Invoke();
    }

    public void RemoveAt(int index)
    {
        T obj = list[index];
        list.RemoveAt(index);
        ChangeRemove?.Invoke(obj);
        ChangeAny?.Invoke();
    }

    public void Changed(int index) {
        Change?.Invoke(list[index]);
        ChangeAny?.Invoke();
    }

    public void Changed(object value) {
        Change?.Invoke((T)value);
        ChangeAny?.Invoke();
    }

    public void CopyTo(Array array, int index)
    {
        list.CopyTo(array as T[], index);
    }

    public IEnumerator GetEnumerator()
    {
        return list.GetEnumerator();
    }

    public void SetLocked(bool locked)
    {
        isLocked = locked;
    }

    public virtual bool WouldReceive(T element)
    {
        if (IsLocked)
            return false;

        if (MaxCount < 0 || Count < MaxCount)
            return true;

        return false;
    }

    public void ClickedOn(int index)
    {
        Debug.Log("clicked on " + index);
    }
}
