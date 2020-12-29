using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableEntityList : IList
{
    public Action<UseableEntity> ChangeRemove;
    public Action<UseableEntity> ChangeAdd;

    //implementation of the IList interface using the 'object' class
    List<object> list = new List<object>();

    public bool IsFixedSize => false;
    public bool IsReadOnly => false;

    public int Count => list.Count;

    public bool IsSynchronized => false;

    public object SyncRoot => null;

    public object this[int index] { get => list[index]; set { list[index] = value; }}

    public int Add(object value)
    {
        list.Add(value);

        if (value as UseableEntity != null)
            ChangeAdd?.Invoke(value as UseableEntity);

        return list.IndexOf(value);
    }

    public void Clear()
    {
        list.Clear();
    }

    public bool Contains(object value)
    {
        return list.Contains(value);
    }

    public int IndexOf(object value)
    {
        return list.IndexOf(value);
    }

    public void Insert(int index, object value)
    {
        list.Insert(index, value);
    }

    public void Remove(object value)
    {
        list.Remove(value);

        if (value as UseableEntity != null)
            ChangeRemove?.Invoke(value as UseableEntity);
    }

    public void RemoveAt(int index)
    {
        list.RemoveAt(index);
    }

    public void CopyTo(Array array, int index)
    {
        list.CopyTo(array as object[], index);
    }

    public IEnumerator GetEnumerator()
    {
        return list.GetEnumerator();
    }
}
