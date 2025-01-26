using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    public Slider progressBar;
    public TMP_Text progressText;

    private void Start()
    {
        FXManager.Instance.PlayBgm(BGM_TYPE.TITLE);
    }

    public void SetText(string text)
    {
        progressText.text = text;
    }
    
    public void SetProgress(float value)
    {
        progressBar.value = value;
    }
}
