using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Player : Unit
{
    public float _range;
    public SpriteRenderer weaponSlot;

    private PlayerController _playerController;
    private Dictionary<WEAPON_TYPE, Weapon> _weapons;

    protected override void Die()
    {
        GameManager.Instance.TogglePause();
        UIManager.Instance.ShowPopup(POPUP_TYPE.POPUP_DEAD);
    }

    private void Start()
    {
        base.OnUse();
        _weapons = new();

        _playerController = GetComponent<PlayerController>();
    }

    public void Reset()
    {
        ExperienceManager.Instance.Reset();
        OnUse();
        weaponSlot.sprite = null;
        foreach (var item in _weapons)
        {
            item.Value.isActive = false;
        }
    }

    public void UpgradeWeapon(WeaponDatabase.WeaponData data, int index)
    {
        Weapon currentWeapon = null;

        bool isFirst = !_weapons.ContainsKey(data.type);
        if (isFirst)
        {
            currentWeapon = Instantiate(data.weaponPrefab, this.transform);
            currentWeapon.SetRenderer(weaponSlot);
            _weapons.Add(data.type, currentWeapon);
        }

        _weapons[data.type].Upgrade(data, index);
    }

    public bool HasWeapon(WEAPON_TYPE type)
    {
        return _weapons.ContainsKey(type) && _weapons[type].isActive;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}