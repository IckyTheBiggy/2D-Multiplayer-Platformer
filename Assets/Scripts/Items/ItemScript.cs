using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IPickupable
{
    [System.Serializable]
    public struct AffectStatItem
    {
        public PlayerStats.StatTypes Type;
        public float Amount;
    }
    
    [SerializeField] private List<AffectStatItem> _types;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private ParticleSystem _itemPickupParticles;
    
    [SerializeField] private bool _isMultiplyer;
    [SerializeField] private float _increaseAmount;

    [SerializeField] private float _bobSpeed;
    [SerializeField] private float _bobHeight;

    private Vector3 _startPosition;

    private void Start() => _startPosition = _sprite.transform.localPosition;

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * _bobSpeed) * _bobHeight;

        _sprite.transform.localPosition = _startPosition + new Vector3(0f, yOffset, 0f);
    }

    public void Pickup()
    {
        if (!_isMultiplyer)
            AddStats(GameManager.Instance.PlayerStats, _increaseAmount);
        else
            MultiplyStats(GameManager.Instance.PlayerStats, _increaseAmount);

        Instantiate(_itemPickupParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void AddStats(PlayerStats playerStats, float amount)
    {
        foreach (var type in _types)
        {
            GameManager.Instance.PlayerStats.AffectStat(type.Type, type.Amount);
        }
    }

    private void MultiplyStats(PlayerStats playerStats, float amount)
    {
        foreach (var type in _types)
        {
           GameManager.Instance.PlayerStats.AffectStat(type.Type, type.Amount, true);
        }
    }
}
