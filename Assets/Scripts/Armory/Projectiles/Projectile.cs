using Enums;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;
    public int damage = 10;

    public int WeaponDamage { private get; set; }
    private Vector3 _direction;
    private bool _hasCollided = false;

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

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(_direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_hasCollided && collision.CompareTag("Enemy"))
        {
            _hasCollided = true;
            collision.GetComponent<Enemy>().TakeDamage(damage + WeaponDamage);
            FXManager.Instance.PlayVfx(transform.position);
            FXManager.Instance.PlaySfx(SFX_TYPE.HIT);
            Destroy(gameObject);
        }
    }
}