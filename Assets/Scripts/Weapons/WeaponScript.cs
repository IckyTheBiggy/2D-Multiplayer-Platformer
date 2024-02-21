using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private PhotonView _pv;
    
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] private ParticleSystem _muzzleFlash;

    [SerializeField] private float _fireRate;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletPrefab;

    private Vector3 _startPos;
    private float _initialFireRate;
    private float _timeToNextShot;

    void Start()
    {
        _startPos = transform.localPosition;
        _initialFireRate = _fireRate;
    }

    void Update()
    {
        if (!_pv.IsMine)
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

        var bullet =
            PhotonNetwork.Instantiate(_bulletPrefab.name, _gunPoint.position, _gunPoint.rotation);

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        bulletScript.BulletSpeed *= _player.GetComponent<PlayerStats>().GetStatAmount(PlayerStats.StatTypes.Range);
        bulletScript.Damage =
            _damage * _player.GetComponent<PlayerStats>().GetStatAmount(PlayerStats.StatTypes.Damage);
        bullet.GetComponent<Rigidbody2D>().velocity += _player.GetComponent<Rigidbody2D>().velocity / 2;

        _timeToNextShot = _fireRate;
    }

    private void ShootTimer()
    {
        if (_timeToNextShot > 0)
            _timeToNextShot -= Time.deltaTime;
    }
}
