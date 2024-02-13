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
    
    [System.Serializable]
    public struct StatItem
    {
        public float Health;
        public float MaxSpeed;
        public float Speed;
        public float ShootSpeed;
        public float Range;
        public float Damage;
    }

    public StatItem Stats;
    
    [HideInInspector] public float CurrentHealth;

    public void AffectStat(StatTypes type, float amount, bool multiply = false)
    {
        if (multiply)
        {
            if (type == StatTypes.Health)
                Stats.Health *= amount;
        
            if (type == StatTypes.MaxSpeed)
                Stats.MaxSpeed *= amount;
        
            if (type == StatTypes.Speed)
                Stats.Speed *= amount;
        
            if (type == StatTypes.ShootSpeed)
                Stats.ShootSpeed *= amount;
        
            if (type == StatTypes.Range)
                Stats.Range *= amount;
        
            if (type == StatTypes.Damage)
                Stats.Damage *= amount;
        }

        else
        {
            if (type == StatTypes.Health)
                Stats.Health += amount;
        
            if (type == StatTypes.MaxSpeed)
                Stats.MaxSpeed += amount;
        
            if (type == StatTypes.Speed)
                Stats.Speed += amount;
        
            if (type == StatTypes.ShootSpeed)
                Stats.ShootSpeed += amount;
        
            if (type == StatTypes.Range)
                Stats.Range += amount;
        
            if (type == StatTypes.Damage)
                Stats.Damage += amount;
        }
    }

    private void Start()
    {
        CurrentHealth = Stats.Health;
        _uiManager.UpdateHealthBar();
    }

    private void KillPlayer()
    {
        transform.position = Vector2.zero;
        CurrentHealth = Stats.Health;
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
