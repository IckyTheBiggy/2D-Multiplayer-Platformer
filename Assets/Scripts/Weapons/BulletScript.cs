using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ParticleSystem _bulletCollisionParticles;
    [SerializeField] private string _collisionTag;
    
    [SerializeField] private float _bulletLifetime;
    [SerializeField] private bool _isEnemyBullet;

    public float BulletSpeed;
    [HideInInspector] public float Damage;

    private float _bulletTime;

    private void Start()
    {
        _rb.AddForce(transform.right * BulletSpeed);

        StartCoroutine(DestoryBulletRoutine());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            
            if (damageable != null)
                damageable.TakeDamage(Damage);
        }

        if (other.CompareTag("Player") && _isEnemyBullet)
        {
            IDamageable damageable = other.GetComponentInChildren<IDamageable>();
            
            if (damageable != null)
                damageable.TakeDamage(Damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag(_collisionTag))
        {
            //PhotonNetwork.Instantiate(_bulletCollisionParticles.name, transform.position, Quaternion.identity);
            //PhotonNetwork.Destroy(gameObject);
        }
    }

    private IEnumerator DestoryBulletRoutine()
    {
        yield return new WaitForSecondsRealtime(_bulletLifetime);
        //PhotonNetwork.Destroy(gameObject);
    }
}
