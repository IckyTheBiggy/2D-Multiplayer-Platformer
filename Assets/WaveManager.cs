using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    public struct EnemyItem
    {
        public GameObject EnemyPrefab;
        public float SpawnChance;
    }
    
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<EnemyItem> _enemies;
    
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private int _timeBetweenEnemySpawns;
    [SerializeField] private int _enemiesToSpawn;
    
    [HideInInspector] public int EnemiesLeft;
    [HideInInspector] public int TimeToNextWave;
    
    private int _currentWave;
    private int _currentFloor;
    private bool _canSpawnNextWave;
    
    void Start()
    {
        _canSpawnNextWave = true;
    }

    private void Update()
    {
        if (_canSpawnNextWave && EnemiesLeft <= 0)
        {
            SpawnItem();
            StartCoroutine(SpawnWaveRoutine());
            _canSpawnNextWave = false;
            Debug.Log("Next wave spawning...");
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = GetRandomEnemy();
        Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity);
        Debug.Log(enemyPrefab);
    }
    
    private GameObject GetRandomEnemy()
    {
        float spawnChance = Random.Range(0f, 1f);
        float cumulativeSpawnRate = 0f;

        foreach (var enemy in _enemies)
        {
            cumulativeSpawnRate += enemy.SpawnChance;

            if (spawnChance <= cumulativeSpawnRate)
                return enemy.EnemyPrefab;
        }

        return null;
    }

    private void SpawnItem()
    {
        
    }

    private IEnumerator SpawnWaveRoutine()
    {
        yield return new WaitForSecondsRealtime(_timeBetweenWaves);
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        int spawnedEnemies = 0;
        
        while (_enemiesToSpawn > spawnedEnemies)
        {
            SpawnEnemy();
            EnemiesLeft++;
            spawnedEnemies++;
            GameManager.Instance.UIManager.UpdateEnemiesLeftText();
            yield return new WaitForSecondsRealtime(_timeBetweenEnemySpawns);
        }

        _currentWave++;
        _canSpawnNextWave = true;
        Debug.Log(_currentWave);

        yield return null;
    }
}
