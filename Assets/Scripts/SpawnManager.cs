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
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}