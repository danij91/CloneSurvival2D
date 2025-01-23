using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public Projectile projectile;
    public List<Projectile> projectiles = new();

    protected override void Attack(Vector2 direction)
    {
        Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.WeaponDamage = damage;
        newProjectile.SetDirection(direction);
        projectiles.Add(newProjectile);
    }
    
    protected virtual void Initialize()
    {
        base.Initialize();
    }
}