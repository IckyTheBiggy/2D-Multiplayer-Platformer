using System.Collections;
using System.Collections.Generic;
using FishNet.Managing.Server;
using FishNet.Object;
using UnityEngine;

public class WeaponScript : NetworkBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] private ParticleSystem _muzzleFlash;

    [SerializeField] private float _fireRate;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletPrefab;

    private List<GameObject> _spawnedBullets = new List<GameObject>();
    
    private Vector3 _startPos;
    private float _initialFireRate;
    private float _timeToNextShot;

    private float _bulletSpeed;

    void Start()
    {
        _startPos = transform.localPosition;
        _initialFireRate = _fireRate;
    }

    void Update()
    {
        if (!IsOwner)
            return;
        
        ShootTimer();
        //_fireRate = _initialFireRate / GameManager.Instance.PlayerStats.GetStatAmount(PlayerStats.StatTypes.ShootSpeed);

        if (Input.GetKey(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot()
    {
        if (_timeToNextShot > 0)
            return;

        Vector3 mousePosition = _playerManager.Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = (mousePosition - _player.transform.position).normalized;

        Vector3 startPosition = _gunPoint.position;
        
         SpawnBulletLocally(_gunPoint.position, shootDirection);
         SpawnBullet(_gunPoint.position, shootDirection, TimeManager.Tick);

        _timeToNextShot = _fireRate;
    }

    private void ShootTimer()
    {
        if (_timeToNextShot > 0)
            _timeToNextShot -= Time.deltaTime;
    }

    private void SpawnBulletLocally(Vector3 startPosition, Vector3 direction)
    {
        var bullet =
            Instantiate(_bulletPrefab, startPosition, _gunPoint.rotation);

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        bulletScript.BulletSpeed *= _player.GetComponent<PlayerStats>().GetStatAmount(PlayerStats.StatTypes.Range);
        bulletScript.Damage =
            _damage * _player.GetComponent<PlayerStats>().GetStatAmount(PlayerStats.StatTypes.Damage);
        bullet.GetComponent<Rigidbody2D>().velocity += _player.GetComponent<Rigidbody2D>().velocity / 2;

        _bulletSpeed = bulletScript.BulletSpeed;
        StartCoroutine(bulletScript.DestoryBulletRoutine());
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
        //Vector3 spawnPosition = startPosition * _bulletSpeed * timeDifferance;

        GameObject bullet = Instantiate(_bulletPrefab, _gunPoint.position, _gunPoint.rotation);
        StartCoroutine(bullet.GetComponent<BulletScript>().DestoryBulletRoutine());
        _spawnedBullets.Add(bullet);
    }
}
