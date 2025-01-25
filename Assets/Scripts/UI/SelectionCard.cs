using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectionCard : MonoBehaviour
{
    public Button btn_select;
    public Image img_icon;
    public TMP_Text txt_option;

    private int _weaponIndex;
    private int _upgradeOptionIndex;

    public void SetIcon(Sprite icon)
    {
        img_icon.sprite = icon;
    }

    public void SetOptionName(string option)
    {
        txt_option.text = option;
    }

    public void SetOnBtnClick(Action onclick)
    {
        btn_select.onClick.AddListener(() => onclick?.Invoke());
    }

}
