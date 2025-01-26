using Enums;
using UnityEngine;

public class MagicBall : Projectile
{
    private float _range;

    public void SetWeaponRange(float range)
    {
        _range = range;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_hasCollided && collision.CompareTag("Enemy"))
        {
            Restore();
            _hasCollided = true;
            bool isHit = false;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _range);

            foreach (Collider2D collider in hitColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().TakeDamage(_weaponDamage);
                    isHit = true;
                }
            }

            if (isHit)
            {
                FXManager.Instance.PlaySfx(SFX_TYPE.HIT);
            }

            FXManager.Instance.PlayVfx(transform.position, VFX_TYPE.EXPLOSION, _range);
            FXManager.Instance.PlaySfx(SFX_TYPE.EXPLOSION);
        }
    }
    
    protected override void OnUse()
    {
        _speed = 3f;
        _lifeTime = 5f;
        _currentTime = 0;
    }
}