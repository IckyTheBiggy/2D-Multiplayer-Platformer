using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyAIScript : MonoBehaviour, IDamageable
{
    [Header("Referances")] 
    
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private ParticleSystem _damageParticles;
    
    [Header("Values")] 
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private bool _canShoot;

    private float _health;
    private float _attackGracePeriod;
    private bool _canAttack;
    private bool _alive;

    private void Start()
    {
        _health = _maxHealth;
        _alive = true;
    }

    private void Update()
    {
        GameObject closestPlayer = FindClosestPlayer();

        if (closestPlayer != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, closestPlayer.transform.position);

            if (distanceToPlayer <= _attackDistance)
            {
                if (_attackGracePeriod <= 0 && !_canShoot)
                {
                    DamagePlayer(closestPlayer);
                    _attackGracePeriod = 1f / _attackSpeed;
                }

                if (_attackGracePeriod <= 0 && _canShoot)
                {
                    Vector3 shootDirection = (transform.position - closestPlayer.transform.position).normalized;
                    
                    float angle = Mathf.Atan2(-shootDirection.y, -shootDirection.x) * Mathf.Rad2Deg;
                    Quaternion direction = Quaternion.AngleAxis(angle, Vector3.forward);
                    
                    ShootPlayer(direction);
                    _attackGracePeriod = 1f / _attackSpeed;
                }

                else
                    MoveTowardsPlayer(closestPlayer.transform.position);
            }

            else
                MoveTowardsPlayer(closestPlayer.transform.position);
        }
        else
        {
            //Wandering Logic
        }

        _attackGracePeriod -= Time.deltaTime;
    }

    private void MoveTowardsPlayer(Vector2 playerPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, _speed * Time.deltaTime);
    }

    private GameObject FindClosestPlayer()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, _detectionRange, _playerLayer);
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.gameObject;
            }
        }

        return closestPlayer;
    }

    private void ShootPlayer(Quaternion direction)
    {
        var bullet =
        Instantiate(_bulletPrefab, transform.position, direction);

        bullet.GetComponent<BulletScript>().Damage = _damage;
    }
    
    
    public void TakeDamage(float damage)
    {
        //_pv.RPC("DamageEnemyRpc", RpcTarget.All, damage);
        
        if (_health <= 0)
        {
            if (_alive)
            {
                GameManager.Instance.WaveManager.EnemiesLeft--;
                //GameManager.Instance.UIManager.UpdateEnemiesLeftText();
                _alive = false;
                //PhotonNetwork.Destroy(gameObject);
            }
        }

        //PhotonNetwork.Instantiate(_damageParticles.name, transform.position, Quaternion.identity);
    }

    //[PunRPC]
    private void DamageEnemyRpc(float damage)
    {
        _health -= damage;
    }

    private void DamagePlayer(GameObject player)
    {
        IDamageable damageable = player.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(_damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canAttack = false;
        }
    }
}
