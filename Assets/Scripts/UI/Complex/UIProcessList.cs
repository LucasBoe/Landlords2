using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProcessList : UIList<Process>
{
    public int HighlightElement
    {
        set
        {
            foreach (UIListElement<Process> element in elementRepresentations)
                element.SetSelected(elementRepresentations.IndexOf(element) == value);
        }
    }
}
