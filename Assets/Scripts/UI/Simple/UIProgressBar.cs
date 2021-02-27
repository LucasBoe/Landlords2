using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIProgressBar : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image progressImage;
    [SerializeField] TMP_Text text;

    IUIProgress connected;

    bool updateBar;
    float startTime;
    float endTime;
    public void Init(IUIProgress progress)
    {
        if (connected != null)
        {
            connected.OnStartProgressEvent -= StartProgress;
            connected.OnFinishProgressEvent -= EndProgress;
        }

        connected = progress;
        connected.OnStartProgressEvent += StartProgress;
        connected.OnFinishProgressEvent += EndProgress;

        if (connected.IsInProgress)
            StartProgress();
        else
            EndProgress();
    }

    private void OnDisable()
    {
        if (connected != null)
        {
            connected.OnStartProgressEvent -= StartProgress;
            connected.OnFinishProgressEvent -= EndProgress;
        }
    }

    private void StartProgress()
    {
        updateBar = true;
        startTime = connected.GetStartTime();
        endTime = startTime + connected.GetDuration();

        SetVisualsActive(true);
    }

    private void EndProgress()
    {
        updateBar = false;
        SetVisualsActive(false);
    }

    private void SetVisualsActive(bool active)
    {
        backgroundImage.enabled = active;
        progressImage.enabled = active;
        text.enabled = active;
    }

    private void Update()
    {
        if (updateBar)
        {
            float progress = (Time.time - startTime) / (endTime - startTime);
            UpdateBar(progress);
        }
    }

    public void UpdateBar(float progress)
    {
        progressImage.fillAmount = progress;
        text.text = Mathf.RoundToInt(progress * 100f).ToString() + "%";
        text.alignment = progress > 0.5f ? TextAlignmentOptions.MidlineLeft : TextAlignmentOptions.MidlineRight;
    }
}
