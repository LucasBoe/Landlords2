using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProcessorBase : UIBehaviour
{
    [SerializeField] protected Processor processor;
    [SerializeField] protected UIProgressBar progressBarUI;

    public virtual void SwitchToFullUI()
    {
        Game.UIHandler.CreateUI<UIProcessor>(GetConnected<Processor>());
        Destroy(gameObject);
    }

    public virtual void SwitchToDotUI()
    {
        Game.UIHandler.CreateUI<UIProcessorDot>(GetConnected<Processor>());
        Destroy(gameObject);
    }
}
