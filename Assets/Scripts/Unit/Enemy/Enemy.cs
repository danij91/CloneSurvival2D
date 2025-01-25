using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Enemy : Unit
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _basicDamage;
    [SerializeField] private int _experience;
    [SerializeField] private Enums.MOVEMENT_TYPE _movementType;

    private Transform _playerTransform;
    private bool _isStop;

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (_isStop)
        {
            return;
        }

        switch (_movementType)
        {
            case MOVEMENT_TYPE.TRACKING:
                Vector2 direction = (_playerTransform.position - transform.position).normalized;
                transform.position += (Vector3)direction * (_moveSpeed * Time.deltaTime);
                break;
            case MOVEMENT_TYPE.LINEAR:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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
        Restore();
    }

    protected virtual int CalculateDamage()
    {
        return _basicDamage;
    }

    private void StopMovement()
    {
        _isStop = true;
    }

    private void StartMovement()
    {
        _isStop = false;
    }


    internal override void OnInitialize(params object[] parameters)
    {
        if (parameters.Length > 0)
        {
            var enemySpec = (EnemySpawnDatabase.EnemySpec)parameters[0];
            maxHealth = enemySpec.health;
            _basicDamage = enemySpec.damage;
            _movementType = enemySpec.movementType;
            _moveSpeed = enemySpec.speed;
        }

        _playerTransform = GameManager.Instance.playerController.transform;
        OnPlayDamageEffect = StopMovement;
        OnCompleteDamageEffect = StartMovement;
        OnDamaged = () => { FXManager.Instance.PlaySfx(Enums.SFX_TYPE.HIT); };
    }

    protected override void OnRestore()
    {
    }
}