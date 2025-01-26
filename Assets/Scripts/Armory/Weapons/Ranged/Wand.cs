using UnityEngine;

public class Wand : RangedWeapon
{
    private float _range;

    protected override void Attack(Vector2 direction)
    {
        base.Attack(direction);
        FXManager.Instance.PlaySfx(Enums.SFX_TYPE.FIRE);
        var magicBall =
            PoolingManager.Instance.Create<MagicBall>(POOL_TYPE.Projectile, _playerTransform.position, "Magicball");
        magicBall.SetWeaponDamage(_damage);
        magicBall.SetWeaponRange(_range);
        magicBall.SetDirection(direction);
    }

    protected override void SetFirstRange(float value)
    {
        _range = value;
    }

    protected override void UpgradeRange(float value)
    {
        _range += value;
    }
}