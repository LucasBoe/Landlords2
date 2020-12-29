using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIConnectable
{
    void Init<T>(T obj);
    void UpdateUI();
    void HideUI();
    T GetConnected<T>();
}

public interface IUIHighlightable : IUIConnectable {

     void SetSelected(bool isSelected);
}

public interface IUIClickable : IUIConnectable {
    void SetClickMethod(System.Action clickMethod);
}

public class UIBehaviour : MonoBehaviour, IUIConnectable
{
    protected object connected;

    public virtual void Init<T>(T obj)
    {
        connected = obj;
    }

    public virtual void UpdateUI() {
        //
    }

    public virtual void HideUI() {
        Destroy(gameObject);
    }

    public T GetConnected<T>()
    {
        return (T)connected;
    }

    public void SetSelected(bool isSelected)
    {
    }
}
