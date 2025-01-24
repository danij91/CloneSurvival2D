using System;
using UnityEngine;

public class Bow : RangedWeapon
{
    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        base.Initialize();
        upgradeOptionNames = new string[] { "damage", "cooldown" };
        upgradeOptionSpecs = new float[] { 5, -0.9f };
    }

    protected override void Attack(Vector2 direction)
    {
        base.Attack(direction);
        FXManager.Instance.PlaySfx(Enums.SFX_TYPE.SHOOT);
    }
}