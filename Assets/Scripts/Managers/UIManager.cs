using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        SubscribeEvents();    
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onReset += OnReset;
        CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccesful;
    }

    private void OnLevelFailed()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
    }

    private void OnLevelSuccessful()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
    }

    private void OnLevelInitialize(byte arg0)
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 0);
        UISignals.Instance.onSetLevelValue?.Invoke((byte)CoreGameSignals.Instance.onGetLevelValue?.Invoke());
    }

    private void OnReset()
    {
        CoreUISignals.Instance.onCloseAllPanels?.Invoke();
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onReset -= OnReset;
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccesful;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();        
    }

    public void Play()
    {
        UISignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(1);
        InputSignals.Instance.onEnableInput?.Invoke();
        CameraSignals.Instance.onSetCameraTarget?.Invoke();
    }

    private void OnStageAreaSuccesful(byte stageValue)
    {
        UISignals.Instance.onSetStageColor?.Invoke(stageValue);
    }

    public void NextLevel()
    {
        CoreGameSignals.Instance.onNextLevel?.Invoke();
    }

    public void RestartLevel()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
    }
}