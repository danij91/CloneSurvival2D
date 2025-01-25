using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : PoolingObject
{
    public ProgressBar2D hpBar;
    private int _currentHealth;
    public int maxHealth;
    public GameObject characterModel;
    public EffectController effectController;

    private const int BLINK_COUNT = 2;
    private const float BLINK_INTERVAL = 0.2f;
    

    public virtual void TakeDamage(int damage)
    {
        _currentHealth = Math.Max(_currentHealth - damage, 0);

        hpBar.UpdateHp(_currentHealth, maxHealth);

        if (_currentHealth == 0)
        {
            Die();
        }
        else
        {
            effectController?.PlayTakeDamageEffect();
        }
    }

    protected abstract void Die();

    internal override void OnInitialize(params object[] parameters)
    {
    }

    protected override void OnUse()
    {
        _currentHealth = maxHealth;
        hpBar.UpdateHp(_currentHealth, maxHealth);
    }

    protected override void OnRestore()
    {
    }
}