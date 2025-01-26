using Enums;
using UnityEngine;

public class Sword : MeleeWeapon
{
    public FixedVfxUnit vfxUnit;
    
    protected override void Attack(UnityEngine.Vector2 direction)
    {
        base.Attack(direction);
        bool isHit = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_playerTransform.position, _range);
        
        vfxUnit.PlayVfx(_range);
        
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().TakeDamage(_damage);
                isHit = true;
            }
        }

        if (isHit)
        {
            FXManager.Instance.PlaySfx(SFX_TYPE.HIT);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}