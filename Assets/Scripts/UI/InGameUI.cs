using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Button btn_pause;
    public Button btn_settings;
    public SelectionCard selectionCardPrefab;
    public RectTransform selectionPanel;
    public Slider experienceBar;
    
    private List<SelectionCard> _selectionCards = new();
    
    private void Start()
    {
        btn_pause.onClick.AddListener(OnClickPause);
        btn_settings.onClick.AddListener(OnClickSettings);
        FXManager.Instance.PlayBgm(BGM_TYPE.INGAME);
    }

    public void SetExperience(float experience)
    {
        experienceBar.value = experience;
    }

    public void SetCardIcon(int index, Sprite icon)
    {
        _selectionCards[index].SetIcon(icon);
    }
    
    public void SetCardOptionName(int index, string optionName)
    {
        _selectionCards[index].SetOptionName(optionName);
    }

    public void ShowSelectionPanel()
    {
        selectionPanel.gameObject.SetActive(true);
    }

    public void HideSelectionPanel()
    {
        selectionPanel.gameObject.SetActive(false);
    }
    
    public void CreateSelectionCard(int optionCount)
    {
        for (int i = 0; i < optionCount; i++)
        {
            SelectionCard newCard = Instantiate(selectionCardPrefab, selectionPanel);
            _selectionCards.Add(newCard);
            int index = i;
            _selectionCards[i].SetOnBtnClick(() => ExperienceManager.Instance.OnSelectCard(index));
        }
    }

    private void OnClickPause()
    {
        GameManager.Instance.TogglePause();
        UIManager.Instance.ShowPopup(POPUP_TYPE.POPUP_PAUSE);
    }

    private void OnClickSettings()
    {
        GameManager.Instance.TogglePause();
        UIManager.Instance.ShowPopup(POPUP_TYPE.POPUP_SETTING);
    }
}