using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IPickupable
{
    [SerializeField] private bool _isMultiplyer;
    [SerializeField] private float _healthIncrease;
    [SerializeField] private float _speedIncrease;
    [SerializeField] private float _shotSpeedIncrease;
    [SerializeField] private float _damageIncrease;
    
    public void Pickup()
    {
        PlayerStats _playerStats =
            GameManager.Instance.Player.GetComponentInChildren<PlayerStats>();
        
        if (!_isMultiplyer)
            AddStats(_playerStats);
        else
            MultiplyStats(_playerStats);
    }

    private void AddStats(PlayerStats playerStats)
    {
        playerStats.Health += _healthIncrease;
        playerStats.Speed += _speedIncrease;
        playerStats.ShootSpeed += _shotSpeedIncrease;
        playerStats.Damage += _damageIncrease;
    }

    private void MultiplyStats(PlayerStats playerStats)
    {
        playerStats.Health *= _healthIncrease;
        playerStats.Speed *= _speedIncrease;
        playerStats.ShootSpeed *= _shotSpeedIncrease;
        playerStats.Damage *= _damageIncrease;
    }
}
