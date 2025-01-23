using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, 5f); // 2초 후 5초마다 적 생성
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position + RandomSpawnPoint(3f), Quaternion.identity);
    }

    private Vector3 RandomSpawnPoint(float length)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector2 randomDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        return randomDirection * length;
    }
}