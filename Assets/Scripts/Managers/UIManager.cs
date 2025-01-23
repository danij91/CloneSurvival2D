using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button btn_pause;
    
    public GameObject _pausePanel;


    private void Start()
    {
        btn_pause.onClick.AddListener(OnClickPause);
    }

    private void OnClickPause()
    {
        GameManager.Instance.TogglePause();
        _pausePanel.SetActive(!_pausePanel.activeSelf);
    }
}