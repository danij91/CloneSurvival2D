using Enums;
using UnityEngine;

public class Hammer : MeleeWeapon
{
    private Vector3 _direction;

    protected override void Attack(Vector2 direction)
    {
        base.Attack(direction);
        bool isHit = false;

        direction *= _range;
        _direction = direction;
        Vector3 stampPosition = _playerTransform.position + (Vector3)direction;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(stampPosition, _range);
        FXManager.Instance.PlayVfx(stampPosition, VFX_TYPE.STAMP, _range);
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
        Gizmos.DrawWireSphere(transform.position + _direction, _range);
    }
}