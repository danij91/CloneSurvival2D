using UnityEngine;

public class MeleeWeapon: Weapon
{
    protected float _range;
    
    protected override void SetFirstRange(float value)
    {
        _range = value;
    }

    protected override void UpgradeRange(float value)
    {
        _range += value;
    }
}