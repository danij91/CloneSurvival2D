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
        public float interval;
        public float radius;
        public float distance;
        public EnemySpec enemySpec;
        public int count;

        public float startTime;
        public float duration;
    }

    [Serializable]
    public class EnemySpec
    {
        public string name;
        public int health;
        public int damage;
        public int speed;
        public Enums.MOVEMENT_TYPE movementType;
    }

    
    public EnemySpawnPattern[] SpawnPatterns;
}