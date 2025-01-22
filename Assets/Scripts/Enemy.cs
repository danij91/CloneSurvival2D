using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector2 direction = (_player.position - transform.position).normalized;
        transform.position += (Vector3) direction * (moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 피격 피해 적용
        }
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 에너미에게 피격 피해 적용
        }
    }
}