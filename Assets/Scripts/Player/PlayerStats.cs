using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public enum StatTypes
    {
        Health,
        MaxSpeed,
        Speed,
        ShootSpeed,
        Range,
        Damage
    }

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private List<Stat> _stats;
    
    public List<Stat> Stats => _stats;
    
    [HideInInspector] public float CurrentHealth;

    public void AffectStat(StatTypes type, float amount, bool multiply = false)
    {
        if (multiply)
            Stats.Find(x => x.Type == type).Value *= amount;
        else
            _stats.Find(x => x.Type == type).Value += amount;
    }

    public float GetStatAmount(StatTypes type)
    {
        return Stats.Find(x => x.Type == type).Value;
    }

    private void Start()
    {
        CurrentHealth = GetStatAmount(StatTypes.Health);
        _uiManager.UpdateHealthBar();
    }

    private void KillPlayer()
    {
        transform.position = Vector2.zero;
        CurrentHealth = GetStatAmount(StatTypes.Health);
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
