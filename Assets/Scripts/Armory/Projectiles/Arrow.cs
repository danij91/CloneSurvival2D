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
            if (_fierceCount >= 0)
            {
                _hasCollided = false;
            }
            else
            {
                _hasCollided = true;
            }
            
            collision.GetComponent<Enemy>().TakeDamage(_weaponDamage);
            FXManager.Instance.PlayVfx(transform.position, VFX_TYPE.HIT);
            FXManager.Instance.PlaySfx(SFX_TYPE.HIT);
        }
    }

    public void ResetFierceCount(int fierceCount)
    {
        _fierceCount = fierceCount;
    }
    
    protected override void OnUse()
    {
        _speed = 5f;
        _lifeTime = 3f;
        _currentTime = 0;
    }
    
    internal override void OnInitialize(params object[] parameters)
    {
    }
}