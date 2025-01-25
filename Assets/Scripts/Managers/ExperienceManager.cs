using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ExperienceManager : Singleton<ExperienceManager>
{
    //레벨업 테스트용 코드
#if UNITY_EDITOR
    public bool ForceWeaponType;
    public WEAPON_TYPE wt;
    public bool ForceWeaponOption = false;
    public int ot;
#endif
    
    public SelectionCard selectionCardPrefab;
    public RectTransform selectionPanel;
    public Slider experienceBar;
    public WeaponDatabase weaponDatabase;

    private int _currentExperience;
    private int _currentMaxExperience = 10;
    private int _currentLevel = 1;
    private Player _player;
    private List<WeaponDatabase.WeaponData> _currentSelectedWeaponData = new();
    private List<int> _currentSelectedOptionIndex = new();
    private List<SelectionCard> _selectionCards = new();
    private int _levelUpCount = 0;

    private const int OPTION_COUNT = 3;

    private void Start()
    {
        _player = GameManager.Instance.playerController.GetComponent<Player>();
        GameManager.Instance.PauseGame();

        for (int i = 0; i < OPTION_COUNT; i++)
        {
            SelectionCard newCard = Instantiate(selectionCardPrefab, selectionPanel);
            _selectionCards.Add(newCard);
            int index = i;
            _selectionCards[i].SetOnBtnClick(() => OnSelectCard(index));
            _currentSelectedWeaponData.Add(null);
            _currentSelectedOptionIndex.Add(0);
        }

        ShowUpgradeSelectionUI(true);
    }

    public void TakeExperience(int experience)
    {
        _currentExperience += experience;

        if (_currentMaxExperience <= _currentExperience)
        {
            LevelUp();
        }

        experienceBar.value = (float)_currentExperience / _currentMaxExperience;
    }

    private void OnSelectCard(int cardIndex)
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
#if UNITY_EDITOR
            if (ForceWeaponType)
            {
                weaponIndex = (int)wt;
            }
            else
            {
                weaponIndex = Random.Range(0, weaponCount);
            }
#else
            weaponIndex = Random.Range(0, weaponCount);
#endif
            selectedWeapon = weaponDatabase.weaponDatas[weaponIndex];
            _currentSelectedWeaponData[i] = selectedWeapon;
            _selectionCards[i].SetIcon(selectedWeapon.icon);

            if (isFirst || !_player.HasWeapon(selectedWeapon.type))
            {
                _selectionCards[i].SetOptionName(selectedWeapon.name);
            }
            else
            {
#if UNITY_EDITOR
                if (ForceWeaponType)
                {
                    _currentSelectedOptionIndex[i] = ot;
                }
                else
                {
                    _currentSelectedOptionIndex[i] = Random.Range(0, selectedWeapon.upgradeOptionDatas.Count);
                }
#else
                // _currentSelectedOptionIndex[i] = Random.Range(0, selectedWeapon.upgradeOptionDatas.Count);
#endif
                _selectionCards[i]
                    .SetOptionName(selectedWeapon.upgradeOptionDatas[_currentSelectedOptionIndex[i]].name);
            }
        }

        selectionPanel.gameObject.SetActive(true);
    }

    private void HideUpgradeSelectionUI()
    {
        selectionPanel.gameObject.SetActive(false);
    }
}