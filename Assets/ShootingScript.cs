using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class ShootingScript : NetworkBehaviour
{
    [SerializeField] private GameObject _bulletPrafab;
    [SerializeField] private float _bulletSpeed;
    
    private List<GameObject> _spawnedBullets = new List<GameObject>();
    
    private void Update()
    {
        foreach (var bullet in _spawnedBullets)
        {
            //bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * 750);
        }
        
        if (!IsOwner)
            return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot()
    {
        Vector3 startPosition = transform.position + transform.right * 0.5f + transform.up;
        Vector3 direction = transform.right;
        
        SpawnBulletLocal(startPosition, direction);
        SpawnBullet(startPosition, direction, TimeManager.Tick);
    }

    private void SpawnBulletLocal(Vector3 startPosition, Vector3 direction)
    {
        GameObject bullet =
            Instantiate(_bulletPrafab, startPosition, Quaternion.identity);
        _spawnedBullets.Add(bullet);
    }

    [ServerRpc]
    private void SpawnBullet(Vector3 startPosition, Vector3 direction, uint startTick)
    {
        SpawnBulletObserver(startPosition, direction, startTick);
    }

    [ObserversRpc(ExcludeOwner = true)]
    private void SpawnBulletObserver(Vector3 startPosition, Vector3 direction, uint startTick)
    {
        float timeDifferance = (float)(TimeManager.Tick - startTick) / TimeManager.TickRate;
        Vector3 spawnPosition = startPosition + direction * _bulletSpeed * timeDifferance;

        GameObject bullet = Instantiate(_bulletPrafab, spawnPosition, Quaternion.identity);
        _spawnedBullets.Add(bullet);
    }
}
