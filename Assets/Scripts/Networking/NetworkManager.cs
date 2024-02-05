using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [HideInInspector] public List<GameObject> Players = new();
    
    [SerializeField] private GameObject _playerPrefab;
    
    public GameObject Player;

    private void Start()
    {
        
    }
}
