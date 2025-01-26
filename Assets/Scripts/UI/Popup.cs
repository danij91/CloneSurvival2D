using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : PoolingObject
{
    public Button btn_close;
    private Transform _canvas;

    private void OnClickClose()
    {
        if (GameManager.Instance.IsPaused())
        {
            GameManager.Instance.ResumeGame();
        }
        Restore();
    }

    internal override void OnInitialize(params object[] parameters)
    {
        btn_close.onClick.AddListener(OnClickClose);
    }

    protected override void OnUse()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
    }

    protected override void OnRestore()
    {
        transform.SetParent(PoolingManager.Instance.transform.Find("Popup"));
    }
}