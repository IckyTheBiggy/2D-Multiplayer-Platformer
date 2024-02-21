using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
    [SerializeField] private float _timeBetweenEnemySetSpawns;
    [SerializeField] private float _timeBetweenEnemySpawns;

    [SerializeField] private int _setsToSpawn;
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
            SpawnItemServerRpc();
            StartCoroutine(SpawnWaveRoutine());
            _canSpawnNextWave = false;
            Debug.Log("Next wave spawning...");
        }
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
            {
                return item.ItemPrefab;
            }
        }
        
        return null;
    }

    [ServerRpc]
    private void SpawnEnemyServerRpc()
    {
        GameObject enemyPrefab = GetRandomEnemy();
        var enemy =
            Instantiate(enemyPrefab, new Vector3(Random.Range(-40f, 60f), 0f, 0f), Quaternion.identity);
        
        enemy.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc]
    private void SpawnItemServerRpc()
    {
        GameObject itemPrefab = GetRandomItem();
        var item =
            Instantiate(itemPrefab, new Vector2(-16f, -4f), Quaternion.identity);
        
        item.GetComponent<NetworkObject>().Spawn();
    }
    
    private IEnumerator SpawnEnemySets()
    {
        int spawnedSets = 0;
        
        while (_setsToSpawn > spawnedSets)
        {
            StartCoroutine(SpawnEnemiesRoutine());
            yield return new WaitForSecondsRealtime(_timeBetweenEnemySetSpawns);
            spawnedSets++;
        }
        
        _currentWave++;
        _canSpawnNextWave = true;
        Debug.Log(_currentWave);

        yield return null;
    }

    private IEnumerator SpawnWaveRoutine()
    {
        yield return new WaitForSecondsRealtime(_timeBetweenWaves);
        StartCoroutine(SpawnEnemySets());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        int spawnedEnemies = 0;
        
        while (_enemiesToSpawn > spawnedEnemies)
        {
            SpawnEnemyServerRpc();
            EnemiesLeft++;
            spawnedEnemies++;
            //GameManager.Instance.UIManager.UpdateEnemiesLeftText();
            yield return new WaitForSecondsRealtime(_timeBetweenEnemySpawns);
        }

        yield return null;
    }
}
