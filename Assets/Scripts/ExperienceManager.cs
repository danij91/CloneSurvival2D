using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperienceManager : MonoBehaviour
{
    public SelectionCard selectionCardPrefab;
    public RectTransform selectionPanel;

    private int _currentExperience;
    private int _currentMaxExperience = 10;
    private int _currentLevel = 1;
    private Player _player;
    private List<int[]> upgradeOptions = new();
    private List<SelectionCard> _selectionCards = new();

    private const int OPTION_COUNT = 3;

    public void TakeExperience(int experience)
    {
        _currentExperience += experience;

        if (_currentMaxExperience > _currentExperience)
        {
            return;
        }

        LevelUp();
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
        Debug.Log($"Card {cardIndex} selected");
        Debug.Log($"Weapon {_player.Weapons[upgradeOptions[cardIndex][0]].weaponName} selected");
        Debug.Log(
            $"Option {_player.Weapons[upgradeOptions[cardIndex][0]].GetOptionName(upgradeOptions[cardIndex][1])} selected");
        Debug.Log(
            $"Spec {_player.Weapons[upgradeOptions[cardIndex][0]].GetOptionSpec(upgradeOptions[cardIndex][1])} selected");
        _player.Weapons[upgradeOptions[cardIndex][0]].Upgrade(upgradeOptions[cardIndex][1]);
        HideUpgradeSelectionUI();
    }


    private void LevelUp()
    {
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

            string weaponName = selectedWeapon.GetOptionName(upgradeOptions[i][1]);
            float weaponSpec = selectedWeapon.GetOptionSpec(upgradeOptions[i][1]);

            _selectionCards[i].SetSelectionSpec(weaponName, "" + weaponSpec);
        }

        selectionPanel.gameObject.SetActive(true);
    }

    private void HideUpgradeSelectionUI()
    {
        selectionPanel.gameObject.SetActive(false);
    }
}