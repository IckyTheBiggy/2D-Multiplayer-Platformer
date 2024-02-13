using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) 
                _instance = new GameObject("Game Manager", typeof(GameManager)).GetComponent<GameManager>();
            return _instance;
        }
        
        
        private set
        {
            if (_instance != null && _instance != value)
            {
                Destroy(value.gameObject);
                return;
            }

            _instance = value;
        }
    }
    
    private void Awake()
    {
        _instance = GetComponent<GameManager>();
    }

    [SerializeField] private GameObject _playerPrefab;

    private Camera _mainCam;
    public Camera MainCam => _mainCam = _mainCam == null ? Camera.main : _mainCam;
    
    public GameObject Player;
    public WaveManager WaveManager;

    [HideInInspector] public PlayerStats PlayerStats;
    [HideInInspector] public UIManager UIManager;

    private void Start()
    {
        PlayerStats = Player.GetComponentInChildren<PlayerStats>();
        UIManager = Player.GetComponentInChildren<UIManager>();
    }
}
