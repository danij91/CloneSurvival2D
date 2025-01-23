using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour
{
    [FormerlySerializedAs("hpBar")] public ProgressBar2D progressBar;
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

        progressBar.UpdateHp(_currentHealth, maxHealth);
    }

    protected abstract void Die();

    protected virtual void Initialize()
    {
        _currentHealth = maxHealth;
    }
}