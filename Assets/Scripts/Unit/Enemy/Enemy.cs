using System;
using UnityEngine;

public abstract class Enemy : Unit
{
    public float moveSpeed = 3f;
    public int basicDamage = 10;
    public int _experience = 10;

    private Transform _playerTransform;

    protected virtual void Update()
    {
        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        transform.position += (Vector3)direction * (moveSpeed * Time.deltaTime);
    }

    protected virtual void Attack(Vector2 direction)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Player player = collision.gameObject.GetComponent<Player>();
        player.TakeDamage(CalculateDamage());
    }

    protected override void Die()
    {
        GameManager.Instance.ExperienceManager.TakeExperience(_experience);
        Destroy(gameObject);
    }

    protected override void Initialize()
    {
        base.Initialize();
        _playerTransform = GameManager.Instance.playerController.transform;
    }

    protected virtual int CalculateDamage()
    {
        return basicDamage;
    }
}