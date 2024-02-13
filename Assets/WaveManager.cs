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
    
    [Serializable]
    public struct Item
    {
        public GameObject ItemPrefab;
        public float DropChance;
    }
    
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<EnemyItem> _enemies;
    [SerializeField] private List<Item> _items;
    
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

    private GameObject GetRandomItem()
    {
        float totalDropChance = 0f;

        foreach (var item in _items)
        {
            totalDropChance += item.DropChance;
        }

        float randomItem = Random.Range(0f, totalDropChance);
        float currentDropChance = 0f;

        foreach (var item in _items)
        {
            currentDropChance += item.DropChance;

            if (randomItem <= currentDropChance)
                return item.ItemPrefab;
        }
        
        return null;
    }

    private void SpawnItem()
    {
        GameObject itemPrefab = GetRandomItem();
        Instantiate(itemPrefab, new Vector2(-16f, -4f), Quaternion.identity);
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
