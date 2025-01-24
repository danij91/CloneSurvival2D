using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour
{
    public ProgressBar2D hpBar;
    private int _currentHealth;
    public int maxHealth;
    public GameObject characterModel;

    private const int BLINK_COUNT = 2;
    private const float BLINK_INTERVAL = 0.2f;
    protected Action OnPlayDamageEffect;
    protected Action OnCompleteDamageEffect;

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
            StartCoroutine(DamageEffect());
        }
    }

    protected IEnumerator DamageEffect()
    {
        OnPlayDamageEffect?.Invoke();

        if (characterModel != null)
        {
            for (int i = 0; i < BLINK_COUNT; i++)
            {
                // 깜빡임 시작 (모델 비활성화)
                characterModel.SetActive(false);
                yield return new WaitForSeconds(BLINK_INTERVAL); // 대기

                // 깜빡임 해제 (모델 활성화)
                characterModel.SetActive(true);
                yield return new WaitForSeconds(BLINK_INTERVAL); // 대기
            }
        }

        OnCompleteDamageEffect?.Invoke();
    }

    protected abstract void Die();

    protected virtual void Initialize()
    {
        _currentHealth = maxHealth;
    }
}