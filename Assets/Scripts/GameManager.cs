using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using Unity.VisualScripting;

public class GameManager : NetworkBehaviour
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

    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    private void Awake()
    {
        _instance = GetComponent<GameManager>();
    }

    private Camera _mainCam;
    public Camera MainCam => _mainCam = _mainCam == null ? Camera.main : _mainCam;
    
    public WaveManager WaveManager;
}
