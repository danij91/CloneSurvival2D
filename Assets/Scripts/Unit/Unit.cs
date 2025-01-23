using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public HpBar2D hpBar;
    private int _currentHealth;
    public int maxHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
            _currentHealth = 0;
        }

        hpBar.UpdateHp(_currentHealth, maxHealth);
    }

    protected abstract void Die();

    protected virtual void Initialize()
    {
        _currentHealth = maxHealth;
    }
}