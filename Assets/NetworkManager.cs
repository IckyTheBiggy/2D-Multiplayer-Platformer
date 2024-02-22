using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    void Start()
    {
        Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
    }
}