using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoverable
{
    void EndHover();
    void StartHover();
}

public interface IClickable
{
    bool IsClickable();
    void Click();
}

public class MouseInteractor : Singleton<MouseInteractor>
{
    [SerializeField] LayerMask ignoreRaycast;

    GameObject currenHoverTEMP;

    IHoverable currentHover;
    IClickable currentAttachable;
    public bool IsDraggingAttachable { get => (currentAttachable != null); }

    private void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, ~ignoreRaycast))
        {
            currenHoverTEMP = hit.collider.gameObject;
            UpdateHover(hit);
        }
        else
        {
            currenHoverTEMP = null;
        }
    }

    private void UpdateHover(RaycastHit hit)
    {
        IClickable clickable = hit.collider.GetComponent<IClickable>();

        //Hover
        IHoverable newDragHover = null;
        newDragHover = hit.collider.GetComponent<IHoverable>();

        if (currentHover != newDragHover)
        {
            if (currentHover != null)
            {
                currentHover.EndHover();
            }

            if (newDragHover != null)
            {
                newDragHover.StartHover();
            }

            currentHover = newDragHover;
        }

        //Mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (clickable != null && clickable.IsClickable())
                ClickOn(clickable);
        }
    }


    private void ClickOn(IClickable clickable)
    {
        clickable.Click();
    }

    private void OnGUI()
    {
        if (currenHoverTEMP != null)
            GUILayout.Box("hover: " + currenHoverTEMP.name);
        else
            GUILayout.Box("no hover.");
    }
}
