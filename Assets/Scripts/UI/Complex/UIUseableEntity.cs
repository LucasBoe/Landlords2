using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUseableEntity : UIListElement<UseableEntity>
{
    [SerializeField] GameObject checkmark;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        checkmark.SetActive(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        checkmark.SetActive(false);
    }
}
