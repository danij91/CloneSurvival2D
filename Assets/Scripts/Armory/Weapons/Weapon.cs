using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float attackCooldown;
    public Sprite weaponIcon;
    
    
    protected string[] upgradeOptionNames;
    protected float[] upgradeOptionSpecs;

    protected float _cooldownTimer;

    public virtual void Upgrade(int selectedOption)
    {
        if (selectedOption == 0)
        {
            damage += 5;
        }
        else
        {
            attackCooldown *= 0.9f;
        }
    }

    protected abstract void Attack(Vector2 direction);


    protected virtual void Update()
    {
        _cooldownTimer -= Time.deltaTime;

        if (!IsAttackReady())
        {
            return;
        }

        Attack(GameManager.Instance.playerController.GetDirection());
        _cooldownTimer = attackCooldown;
    }

    private bool IsAttackReady()
    {
        return _cooldownTimer < 0;
    }

    protected virtual void Initialize()
    {
        _cooldownTimer = attackCooldown;
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