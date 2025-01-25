using System;
using Enums;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private SpriteRenderer _weaponRender;

    protected string weaponName;
    protected int _damage;
    protected float _attackCooldown;
    protected Sprite weaponIcon;
    protected string[] upgradeOptionNames;
    protected float[] upgradeOptionSpecs;
    protected float _cooldownTimer;
    protected Transform _playerTransform;
    
    protected virtual void Update()
    {
        _cooldownTimer -= Time.deltaTime;

        if (!IsAttackReady())
        {
            return;
        }

        Attack(GameManager.Instance.playerController.GetDirection());
        _cooldownTimer = _attackCooldown;
    }
    
    public virtual void Upgrade(WeaponDatabase.WeaponData data, int index, bool isFirst = false)
    {
        if (isFirst)
        {
            weaponName = data.name;
            weaponIcon = data.icon;
            SetFirstDamage(data.damage);
            SetFirstCoolDown(data.cooldown);
            SetFirstRange(data.range);
            SetFirstFierce(data.fierce);
            return;
        }

        var optionData = data.upgradeOptionDatas[index];
        switch (optionData.option)
        {
            case WEAPON_UPGRADE_OPTIONS.DAMAGE:
                UpgradeDamage(optionData.value);
                break;
            case WEAPON_UPGRADE_OPTIONS.RANGE:
                UpgradeRange(optionData.value);
                break;
            case WEAPON_UPGRADE_OPTIONS.COOLDOWN:
                UpgradeCoolDown(optionData.value);
                break;
            case WEAPON_UPGRADE_OPTIONS.FIERCE:
                UpgradeFierce(optionData.value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetRenderer(SpriteRenderer render)
    {
        _weaponRender = render;
    }

    protected virtual void UpgradeDamage(float value)
    {
        _damage += (int)value;
    }
    
    protected virtual void UpgradeCoolDown(float value)
    {
        _attackCooldown *= value;
    }

    protected virtual void UpgradeRange(float value)
    {
        
    }
    
    protected virtual void UpgradeFierce(float value)
    {
        
    }
    
    protected virtual void SetFirstDamage(int value)
    {
        _damage = value;
    }
    
    protected virtual void SetFirstCoolDown(float value)
    {
        _attackCooldown = value;
    }
    
    protected virtual void SetFirstRange(float value)
    {
        
    }
    protected virtual void SetFirstFierce(float value)
    {
        
    }

    protected virtual void Attack(Vector2 direction)
    {
        _weaponRender.sprite = weaponIcon;
        _playerTransform = GameManager.Instance.playerController.transform;
    }

    private bool IsAttackReady()
    {
        return _cooldownTimer < 0;
    }

    protected virtual void Initialize()
    {
        _cooldownTimer = _attackCooldown;
    }

    public string GetOptionName(int selectedOption)
    {
        return upgradeOptionNames[selectedOption];
    }

    public float GetOptionSpec(int selectedOption)
    {
        return upgradeOptionSpecs[selectedOption];
    }

    public int GetOptionCount()
    {
        return upgradeOptionNames.Length;
    }
}