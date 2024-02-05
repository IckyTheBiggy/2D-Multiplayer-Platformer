using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private GameObject _enemyPrefab;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnRate);
            Instantiate(_enemyPrefab, Vector2.zero, Quaternion.identity);
        }
    }
}
