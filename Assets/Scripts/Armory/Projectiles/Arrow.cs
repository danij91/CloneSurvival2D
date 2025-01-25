using Enums;
using UnityEngine;

public class Arrow : Projectile
{
    private int _fierceCount;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_hasCollided && collision.CompareTag("Enemy"))
        {
            _fierceCount--;
            if (_fierceCount <= 0)
            {
                _hasCollided = true;
            }
            
            collision.GetComponent<Enemy>().TakeDamage(_weaponDamage);
            FXManager.Instance.PlayVfx(transform.position, VFX_TYPE.HIT);
            FXManager.Instance.PlaySfx(SFX_TYPE.HIT);
        }
    }
    
    protected override void OnUse()
    {
        _speed = 5f;
        _lifeTime = 3f;
    }
    
    internal override void OnInitialize(params object[] parameters)
    {
        _currentTime = 0;
        if (parameters.Length > 0)
        {
            _fierceCount = (int)parameters[0];
        }
    }
}