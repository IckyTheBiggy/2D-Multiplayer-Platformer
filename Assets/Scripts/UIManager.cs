using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private List<StatItemScript> _statItems;
    
    [SerializeField] private Slider _healthBar;

    public void UpdateHealthBar()
    {
        _healthBar.value = _playerStats.CurrentHealth;
    }

    public void UpdateStatsUI()
    {
        _statItems[0].AssignInfo("Speed: " + _playerStats.Speed.ToString());
        _statItems[1].AssignInfo("Shotspeed: " + _playerStats.ShootSpeed.ToString());
        _statItems[2].AssignInfo("Damage: " + _playerStats.Damage.ToString());
    }
}
