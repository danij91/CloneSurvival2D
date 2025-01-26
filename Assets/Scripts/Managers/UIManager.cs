using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Canvas _canvas;
    private Dictionary<POPUP_TYPE, string> _popupFilePaths = new();
    private Vector2 _screenCenter;
    
    public override void Initialize()
    {
        if (!_isInitialized)
        {
            _popupFilePaths.Add(POPUP_TYPE.POPUP_SETTING, "Popup_Setting");
            _popupFilePaths.Add(POPUP_TYPE.POPUP_PAUSE, "Popup_Pause");
            _popupFilePaths.Add(POPUP_TYPE.POPUP_DEAD, "Popup_Dead");
            _screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }
        
        base.Initialize();
    }

    public void ShowPopup(POPUP_TYPE type)
    {
        if (!_canvas)
        {
            _canvas = FindFirstObjectByType<Canvas>();
        }
        
        PoolingManager.Instance.CreateUI<Popup>(POOL_TYPE.Popup, _screenCenter, _popupFilePaths[type], _canvas.transform, null);
    }

    public Canvas GetCanvas()
    {
        if (!_canvas)
        {
            _canvas = FindFirstObjectByType<Canvas>();
        }

        return _canvas;
    }

    public Vector2 GetScreenCenter()
    {
        return _screenCenter;
    }
}