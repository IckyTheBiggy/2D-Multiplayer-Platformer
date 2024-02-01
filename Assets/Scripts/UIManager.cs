using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    
    [SerializeField] private Slider _healthBar;

    public void UpdateHealthBar()
    {
        _healthBar.value = _playerStats.CurrentHealth;
    }
}
