using System;
using UnityEngine;

public class Bow : RangedWeapon
{
    private int _fierce;

    private void Start()
    {
        Initialize();
    }

    protected override void Attack(Vector2 direction)
    {
        base.Attack(direction);
        FXManager.Instance.PlaySfx(Enums.SFX_TYPE.SHOOT);
        var arrow = PoolingManager.Instance.Create<Arrow>(POOL_TYPE.Projectile, _playerTransform.position, "Arrow",
            null, _fierce);
        arrow.SetWeaponDamage(_damage);
        arrow.SetDirection(direction);
    }

    protected override void SetFirstFierce(float value)
    {
        _fierce = (int)value;
    }

    protected override void UpgradeFierce(float value)
    {
        _fierce += (int)value;
    }
}