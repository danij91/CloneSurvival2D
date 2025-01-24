using System;
using UnityEngine;

public abstract class Enemy : Unit
{
    public float moveSpeed = 3f;
    public int basicDamage = 10;
    public int _experience = 10;

    private Transform _playerTransform;
    private bool _isStop;

    protected virtual void Update()
    {
        if (_isStop)
        {
            return;
        }

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
        ExperienceManager.Instance.TakeExperience(_experience);
        Destroy(gameObject);
    }

    protected override void Initialize()
    {
        base.Initialize();
        _playerTransform = GameManager.Instance.playerController.transform;
        OnPlayDamageEffect = Stop;
        OnCompleteDamageEffect = Move;
        OnDamaged = ()=> { FXManager.Instance.PlaySfx(FXManager.SFX_TYPE.HIT); };
    }

    protected virtual int CalculateDamage()
    {
        return basicDamage;
    }

    private void Stop()
    {
        _isStop = true;
    }

    private void Move()
    {
        _isStop = false;
    }
}