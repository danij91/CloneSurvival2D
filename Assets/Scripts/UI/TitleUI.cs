using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public Button btn_play;

    private void Awake()
    {
        btn_play.onClick.AddListener(OnClickPlay);
    }

    private void Start()
    {
    }

    private void OnClickPlay()
    {
        SceneManager.Instance.LoadScene("InGame");
    }
}