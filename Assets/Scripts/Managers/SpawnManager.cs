using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;
using EnemySpawnPattern = EnemySpawnDatabase.EnemySpawnPattern;

public class SpawnManager : MonoBehaviour
{
    public EnemySpawnDatabase spawnDatabase;

    private List<PatternState> activePatterns = new();
    private Transform _playerTransform;

    private class PatternState
    {
        public float nextSpawnTime;
        public float endTime;
        public EnemySpawnPattern pattern;
        public bool isActive;
    }

    private void Start()
    {
        _playerTransform = GameManager.Instance.playerController.transform;

        foreach (var pattern in spawnDatabase.SpawnPatterns)
        {
            var state = new PatternState
            {
                nextSpawnTime = pattern.startTime,
                endTime = pattern.startTime + pattern.duration,
                pattern = pattern,
                isActive = false
            };

            activePatterns.Add(state);
        }
    }

    private void Update()
    {
        float currentTime = Time.time;

        foreach (var state in activePatterns)
        {
            if (!state.isActive && currentTime >= state.pattern.startTime)
            {
                state.isActive = true;
            }

            if (state.isActive && currentTime >= state.nextSpawnTime)
            {
                Spawn(state.pattern);
                state.nextSpawnTime += state.pattern.interval;
            }

            if (state.isActive && currentTime >= state.endTime)
            {
                state.isActive = false;
            }
        }
    }

    private void Spawn(EnemySpawnPattern pattern)
    {
        Vector3[] spawnPoints;
        Vector3 center = CalculateCenterPosition(pattern.centerType, pattern.distance);
        switch (pattern.type)
        {
            case SPAWN_TYPE.SPOT:
                spawnPoints = new Vector3[] { center };
                SpawnEnemy(spawnPoints, pattern.enemySpec);
                break;
            case SPAWN_TYPE.SPOT_RANDOM:
                spawnPoints = RandomSpawnPoints(center, pattern.count, pattern.radius);
                SpawnEnemy(spawnPoints, pattern.enemySpec);
                break;
            case SPAWN_TYPE.CIRCULAR:
                spawnPoints = CircularSpawnPoints(center, pattern.count, pattern.radius);
                SpawnEnemy(spawnPoints, pattern.enemySpec);
                break;
            case SPAWN_TYPE.LINEAR_VERTICAL:
                spawnPoints = LinearSpawnPoints(center, pattern.count, pattern.radius, 90);
                SpawnEnemy(spawnPoints, pattern.enemySpec);
                break;
            case SPAWN_TYPE.LINEAR_HORIZONTAL:
                spawnPoints = LinearSpawnPoints(center, pattern.count, pattern.radius, 0);
                SpawnEnemy(spawnPoints, pattern.enemySpec);
                break;
            case SPAWN_TYPE.LINEAR_RANDOM:
                float angle = Random.Range(0f, Mathf.PI * 2f);

                spawnPoints = LinearSpawnPoints(center, pattern.count, pattern.radius, angle);
                SpawnEnemy(spawnPoints, pattern.enemySpec);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(pattern.type), pattern.type, null);
        }
    }

    private Vector3 CalculateCenterPosition(SPAWN_CENTER_TYPE type, float distance)
    {
        Vector3 center = _playerTransform.position;
        switch (type)
        {
            case SPAWN_CENTER_TYPE.NONE:
            case SPAWN_CENTER_TYPE.CENTER:
                break;
            case SPAWN_CENTER_TYPE.CENTER_RANDOM:
                float angle = Random.Range(0f, Mathf.PI * 2f);

                float x = Mathf.Cos(angle) * distance;
                float y = Mathf.Sin(angle) * distance;

                center += new Vector3(x, y, 0);
                break;
            case SPAWN_CENTER_TYPE.CENTER_TOP:
                center += Vector3.up * distance;
                break;
            case SPAWN_CENTER_TYPE.CENTER_BOTTOM:
                center += Vector3.down * distance;
                break;
            case SPAWN_CENTER_TYPE.CENTER_LEFT:
                center += Vector3.left * distance;
                break;
            case SPAWN_CENTER_TYPE.CENTER_RIGHT:
                center += Vector3.right * distance;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return center;
    }

    private void SpawnEnemy(Vector3[] positions, EnemySpawnDatabase.EnemySpec enemySpec)
    {
        foreach (var pos in positions)
        {
            PoolingManager.Instance.Create<Slime>(POOL_TYPE.Enemy, pos, enemySpec.name, null, enemySpec);
        }
    }

    private Vector3[] RandomSpawnPoints(Vector3 center, int count, float radius = 0f)
    {
        Vector3[] points = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Random.Range(0f, radius);

            float x = center.x + Mathf.Cos(angle) * distance;
            float y = center.y + Mathf.Sin(angle) * distance;

            float z = center.y;

            points[i] = new Vector3(x, y, z);
            if (IsCloseToPlayer(points[i]))
            {
                i--;
            }
        }

        return points;
    }

    private bool IsCloseToPlayer(Vector3 pos)
    {
        return ((Vector2)pos - (Vector2)_playerTransform.position).sqrMagnitude <
               Mathf.Pow(spawnDatabase.minimumSpawnDistance, 2);
    }

    private Vector3[] CircularSpawnPoints(Vector3 center, int count, float radius)
    {
        Vector3[] points = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2f / count;

            float x = center.x + Mathf.Cos(angle) * radius;
            float y = center.y + Mathf.Sin(angle) * radius;

            points[i] = new Vector3(x, y, center.z);
        }

        return points;
    }

    private Vector3[] LinearSpawnPoints(Vector3 center, int count, float length, float angle)
    {
        Vector3[] points = new Vector3[count];

        float radian = angle * Mathf.Deg2Rad;

        Vector3 direction = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian));

        for (int i = 0; i < count; i++)
        {
            float t = Random.Range(0f, length);
            points[i] = center + direction * t;
        }

        return points;
    }
}