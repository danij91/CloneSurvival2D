using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ExperienceManager : Singleton<ExperienceManager>
{
    public SelectionCard selectionCardPrefab;
    public RectTransform selectionPanel;
    public Slider experienceBar;

    private int _currentExperience;
    private int _currentMaxExperience = 10;
    private int _currentLevel = 1;
    private Player _player;
    private List<int[]> upgradeOptions = new();
    private List<SelectionCard> _selectionCards = new();
    private int _levelUpCount = 0;

    private const int OPTION_COUNT = 3;

    public void TakeExperience(int experience)
    {
        _currentExperience += experience;

        if (_currentMaxExperience <= _currentExperience)
        {
            LevelUp();
        }

        experienceBar.value = (float)_currentExperience / _currentMaxExperience;
    }

    private void Start()
    {
        for (int i = 0; i < OPTION_COUNT; i++)
        {
            SelectionCard newCard = Instantiate(selectionCardPrefab, selectionPanel);
            _selectionCards.Add(newCard);
            int index = i;
            _selectionCards[i].SetOnBtnClick(() => OnSelectCard(index));
        }

        HideUpgradeSelectionUI();

        _player = GameManager.Instance.playerController.GetComponent<Player>();
    }

    private void OnSelectCard(int cardIndex)
    {
        _player.Weapons[upgradeOptions[cardIndex][0]].Upgrade(upgradeOptions[cardIndex][1]);
        _levelUpCount--;
        if (_levelUpCount > 0)
        {
            ShowUpgradeSelectionUI();
        }
        else
        {
            HideUpgradeSelectionUI();
            GameManager.Instance.ResumeGame();
        }
    }


    private void LevelUp()
    {
        FXManager.Instance.PlaySfx(FXManager.SFX_TYPE.LEVEL_UP);
        GameManager.Instance.PauseGame();
        _levelUpCount++;
        _currentExperience -= _currentMaxExperience;
        _currentLevel++;
        _currentMaxExperience = CalculateMaxExperience();
        ShowUpgradeSelectionUI();
    }

    private int CalculateMaxExperience()
    {
        return _currentMaxExperience * 2;
    }

    private void ShowUpgradeSelectionUI()
    {
        int weaponCount = _player.Weapons.Count;
        Weapon selectedWeapon = null;
        for (int i = 0; i < OPTION_COUNT; i++)
        {
            if (upgradeOptions.Count <= i)
            {
                upgradeOptions.Add(new int[3]);
            }

            int weaponIndex = Random.Range(0, weaponCount);
            selectedWeapon = _player.Weapons[weaponIndex];
            upgradeOptions[i][0] = weaponIndex;
            upgradeOptions[i][1] = Random.Range(0, _player.Weapons[weaponIndex].GetOptionCount());

            string optionName = selectedWeapon.GetOptionName(upgradeOptions[i][1]);

            _selectionCards[i].SetSelectionSpec(optionName, selectedWeapon.weaponIcon);
        }

        selectionPanel.gameObject.SetActive(true);
    }

    private void HideUpgradeSelectionUI()
    {
        selectionPanel.gameObject.SetActive(false);
    }
}