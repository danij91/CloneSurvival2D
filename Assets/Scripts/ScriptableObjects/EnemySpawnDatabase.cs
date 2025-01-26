using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnDatabase", menuName = "Database/EnemySpawnDatabase")]
public class EnemySpawnDatabase : ScriptableObject
{
    public float minimumSpawnDistance;
    [Serializable]
    public class EnemySpawnPattern
    {
        public Enums.SPAWN_TYPE type;
        public Enums.SPAWN_CENTER_TYPE centerType;
        public float radius;
        public float distance;
        public EnemySpec enemySpec;
        public int count;

        public float startTime;
        public float duration;
        public float interval;
    }

    [Serializable]
    public class EnemySpec
    {
        public string name;
        public int health;
        public int damage;
        public int speed;
        public int score;
        public Enums.MOVEMENT_TYPE movementType;
    }

    
    public EnemySpawnPattern[] SpawnPatterns;
}