using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    [HideInInspector] public float Damage;
    
    private void Update()
    {
        transform.position += transform.right * (_bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(Damage);
            }
        }
    }
}
