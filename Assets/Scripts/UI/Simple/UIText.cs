using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour, IUIConnectable
{
    object connected;
    TMP_Text text;

    [SerializeField] bool parentIsRoot;
    [SerializeField] string emptyText;

    private void OnEnable()
    {
        text = GetComponent<TMP_Text>();
    }

    public IUIString GetConnected<IUIString>()
    {
        return (IUIString)connected;
    }

    public void HideUI()
    {
        Debug.Log("Hide!");


        if (this == null)
            return;

        if (parentIsRoot)
        {
            Destroy(transform.parent.gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Init<T>(T obj)
    {
        connected = obj;
    }

    public void UpdateUI<T>(T obj)
    {
        connected = obj;

        if (GetConnected<IUIString>() != null)
            text.text = GetConnected<IUIString>().GetUIString();
        else
            text.text = emptyText;
    }
}
