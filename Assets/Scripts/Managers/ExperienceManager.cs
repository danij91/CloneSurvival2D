using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ExperienceManager : Singleton<ExperienceManager>
{
    public WeaponDatabase weaponDatabase;
    
    private int _currentScore;
    private InGameUI _inGameUI;
    private int _currentExperience;
    private int _currentMaxExperience;
    private int _currentLevel;
    private Player _player;
    private List<WeaponDatabase.WeaponData> _currentSelectedWeaponData = new();
    private List<int> _currentSelectedOptionIndex = new();
    private int _levelUpCount = 0;
    private const int MAX_EXPERIENCE = 10;
    private const int OPTION_COUNT = 3;

    public override void Initialize()
    {
        _player = GameManager.Instance.playerController.GetComponent<Player>();
        GameManager.Instance.PauseGame();

        _inGameUI = FindFirstObjectByType<InGameUI>();

        for (int i = 0; i < OPTION_COUNT; i++)
        {
            _currentSelectedWeaponData.Add(null);
            _currentSelectedOptionIndex.Add(0);
        }

        _inGameUI.CreateSelectionCard(OPTION_COUNT);

        Reset();
        base.Initialize();
    }

    public override void Reset()
    {
        _currentScore = 0;
        _currentExperience = 0;
        _currentMaxExperience = MAX_EXPERIENCE;
        _currentLevel = 1;
        _levelUpCount = 0;
        ShowUpgradeSelectionUI(true);
    }

    public void TakeExperience(int experience)
    {
        _currentExperience += experience;

        if (_currentMaxExperience <= _currentExperience)
        {
            LevelUp();
        }

        _inGameUI.SetExperience((float)_currentExperience / _currentMaxExperience);
    }

    public void TakeScore(int score)
    {
        _currentScore += score;
    }

    public int GetScore()
    {
        return _currentScore;
    }

    public void OnSelectCard(int cardIndex)
    {
        _player.UpgradeWeapon(_currentSelectedWeaponData[cardIndex], _currentSelectedOptionIndex[cardIndex]);
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
        GameManager.Instance.PauseGame();
        FXManager.Instance.PlaySfx(Enums.SFX_TYPE.LEVEL_UP);
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

    private void ShowUpgradeSelectionUI(bool isFirst = false)
    {
        int weaponCount = weaponDatabase.weaponDatas.Length;
        WeaponDatabase.WeaponData selectedWeapon;

        int weaponIndex = 0;
        for (int i = 0; i < OPTION_COUNT; i++)
        {
            weaponIndex = Random.Range(0, weaponCount);
            selectedWeapon = weaponDatabase.weaponDatas[weaponIndex];
            _currentSelectedWeaponData[i] = selectedWeapon;
            _inGameUI.SetCardIcon(i, selectedWeapon.icon);

            if (isFirst || !_player.HasWeapon(selectedWeapon.type))
            {
                _inGameUI.SetCardOptionName(i, selectedWeapon.name);
            }
            else
            {
                _currentSelectedOptionIndex[i] = Random.Range(0, selectedWeapon.upgradeOptionDatas.Count);
                _inGameUI.SetCardOptionName(i, selectedWeapon.upgradeOptionDatas[_currentSelectedOptionIndex[i]].name);
            }
        }

        _inGameUI.ShowSelectionPanel();
    }

    private void HideUpgradeSelectionUI()
    {
        _inGameUI.HideSelectionPanel();
    }
}