using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

public class Arrow : Projectile
{
    private int _fierceCount;
    private int _currentFierceCount;
    private List<Enemy> collidedEnemies = new();

    public void ResetFierceCount(int fierceCount)
    {
        _fierceCount = fierceCount;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_hasCollided && collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();

            if (collidedEnemies.Contains(enemy))
            {
                return;
            }
            
            _currentFierceCount++;
            _hasCollided = _fierceCount <= _currentFierceCount;

            collidedEnemies.Add(enemy);

            collision.GetComponent<Enemy>().TakeDamage(_weaponDamage);
            FXManager.Instance.PlayVfx(transform.position, VFX_TYPE.HIT);
            FXManager.Instance.PlaySfx(SFX_TYPE.HIT);
        }

        if (_hasCollided)
        {
            Restore();
        }
    }

    protected override void OnUse()
    {
        _speed = 5f;
        _lifeTime = 3f;
        _currentTime = 0;
        _currentFierceCount = 0;
        _hasCollided = false;
        collidedEnemies.Clear();
    }

    internal override void OnInitialize(params object[] parameters)
    {
    }
}