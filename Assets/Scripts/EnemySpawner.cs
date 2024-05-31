using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    private int waveNumber = 0;
    private int enemiesToSpawn;

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        waveNumber++;
        enemiesToSpawn = waveNumber;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f); // Adjust delay as necessary
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Health enemyHealth = enemyInstance.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.OnDeath += OnEnemyDeath; // Subscribe to the OnDeath event
        }
    }

    void OnEnemyDeath()
    {
        enemiesToSpawn--; // Decrement enemies left to spawn for this wave
        if (enemiesToSpawn <= 0)
        {
            Debug.Log("Wave " + waveNumber + " completed. Starting next wave...");
            StartNextWave();
        }
    }
}
