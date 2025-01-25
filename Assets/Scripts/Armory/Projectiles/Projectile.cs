using Enums;
using UnityEngine;

public class Projectile : PoolingObject
{
    protected float _speed;
    protected float _lifeTime;

    protected int _weaponDamage;
    private Vector3 _direction;
    protected bool _hasCollided = false;

    protected float _currentTime;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }

        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    public void SetWeaponDamage(int damage)
    {
        _weaponDamage = damage;
    }

    private void Update()
    {
        _currentTime+= Time.deltaTime;
        if (_currentTime >= _lifeTime)
        {
            Restore();
        }
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    internal override void OnInitialize(params object[] parameters)
    {
        _currentTime = 0;
    }

    protected override void OnUse()
    {
    }

    protected override void OnRestore()
    {
    }
}