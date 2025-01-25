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

    private const int BLINK_COUNT = 2;
    private const float BLINK_INTERVAL = 0.2f;

    protected Action OnDamaged;
    protected Action OnPlayDamageEffect;
    protected Action OnCompleteDamageEffect;

    public virtual void TakeDamage(int damage)
    {
        OnDamaged?.Invoke();
        _currentHealth = Math.Max(_currentHealth - damage, 0);

        hpBar.UpdateHp(_currentHealth, maxHealth);

        if (_currentHealth == 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(DamageEffect());
        }
    }

    private IEnumerator DamageEffect()
    {
        OnPlayDamageEffect?.Invoke();

        if (characterModel != null)
        {
            for (int i = 0; i < BLINK_COUNT; i++)
            {
                characterModel.SetActive(false);
                yield return new WaitForSeconds(BLINK_INTERVAL);

                characterModel.SetActive(true);
                yield return new WaitForSeconds(BLINK_INTERVAL);
            }
        }

        OnCompleteDamageEffect?.Invoke();
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