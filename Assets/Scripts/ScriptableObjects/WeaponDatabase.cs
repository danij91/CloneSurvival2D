using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Database/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    [Serializable]
    public class WeaponData
    {
        public string name;
        public Weapon weaponPrefab;
        public WEAPON_TYPE type;
        public int damage;
        public float cooldown;
        public Sprite icon;
        public float range;
        public int fierce;
        public List<WeaponUpgradeOptionDatas> upgradeOptionDatas;
    }

    [Serializable]
    public class WeaponUpgradeOptionDatas
    {
        public WEAPON_UPGRADE_OPTIONS option;
        public string name;
        public float value;
    }

    public WeaponData[] weaponDatas;
}
