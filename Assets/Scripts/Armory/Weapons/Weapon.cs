using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float attackCooldown;
    public Sprite weaponIcon;

    protected float _cooldownTimer;

    public void Start()
    {
        _cooldownTimer = attackCooldown;
    }

    protected abstract void Attack(Vector2 direction);

    public virtual void Upgrade()
    {
        damage += 5;
        attackCooldown *= 0.9f;
    }

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
}