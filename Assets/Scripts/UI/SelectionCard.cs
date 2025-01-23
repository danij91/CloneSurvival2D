using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionCard : MonoBehaviour
{
    public Button btn_select;
    public TMP_Text txt_name;
    public TMP_Text txt_spec;

    public void SetSelectionSpec(string name, string spec)
    {
        txt_name.text = name;
        txt_spec.text = spec;
    }

    public void SetOnBtnClick(Action onclick)
    {
        btn_select.onClick.AddListener(() => onclick?.Invoke());
    }

}
