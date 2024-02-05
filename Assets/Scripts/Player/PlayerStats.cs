using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private UIManager _uiManager;
    
    public float Health;
    public float MaxSpeed;
    public float Speed;
    public float ShootSpeed;
    public float Damage;

    [HideInInspector] public float CurrentHealth;

    private void Start()
    {
        CurrentHealth = Health;
        _uiManager.UpdateHealthBar();
        _uiManager.UpdateStatsUI();
    }

    private void KillPlayer()
    {
        transform.position = Vector2.zero;
        CurrentHealth = Health;
        _uiManager.UpdateHealthBar();
    }
    
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        _uiManager.UpdateHealthBar();

        if (CurrentHealth <= 0)
        {
            _uiManager.UpdateHealthBar();
            KillPlayer();
        }
    }
}
