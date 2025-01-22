using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float moveSpeed = 3f;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        transform.position += (Vector3)direction * (moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 피격 피해 적용
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}