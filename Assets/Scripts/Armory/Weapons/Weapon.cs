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

    public abstract void Attack(Vector2 direction);

    public virtual void Upgrade()
    {
        damage += 5;
        attackCooldown *= 0.9f;
    }

    protected virtual void Update()
    {
        _cooldownTimer -= Time.deltaTime;

        if (CanAttack())
        {
            Attack(GameManager.Instance.player.GetDirection());
            _cooldownTimer = attackCooldown;
        }
    }

    public bool CanAttack()
    {
        return _cooldownTimer < 0;
    }
}