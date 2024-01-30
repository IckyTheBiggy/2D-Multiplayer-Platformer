using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPos;
    
    void Start()
    {
        var player =
        PhotonNetwork.Instantiate(_playerPrefab.name, Vector2.zero, Quaternion.identity);
    }
}
