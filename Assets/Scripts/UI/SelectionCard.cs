using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectionCard : MonoBehaviour
{
    public Button btn_select;
    public Image img_icon;
    public TMP_Text txt_option;

    public void SetSelectionSpec(string option,Sprite icon)
    {
        txt_option.text = option;
        img_icon.sprite = icon;
    }

    public void SetOnBtnClick(Action onclick)
    {
        btn_select.onClick.AddListener(() => onclick?.Invoke());
    }

}
