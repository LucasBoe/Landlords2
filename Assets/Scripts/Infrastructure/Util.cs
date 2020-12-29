using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void AddUnique<T>(this IList list, T item)
    {
        if (!list.Contains(item))
            list.Add(item);
    }
}
