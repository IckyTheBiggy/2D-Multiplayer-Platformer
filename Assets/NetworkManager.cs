using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    
    void Start() => PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity);
}
