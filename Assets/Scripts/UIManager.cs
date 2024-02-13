using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private TMP_Text _enemiesLeftText;
    
    [SerializeField] private Slider _healthBar;

    public void UpdateHealthBar()
    {
        _healthBar.value = _playerStats.CurrentHealth;
    }

    public void UpdateEnemiesLeftText()
    {
        _enemiesLeftText.text = GameManager.Instance.WaveManager.EnemiesLeft.ToString();
    }
}
