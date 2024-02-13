using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private PhotonView _pv;
    [SerializeField] private ParticleSystem _muzzleFlash;
    
    [SerializeField] private float _fireRate;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletPrefab;

    private Vector3 _startPos;
    private float _timeToNextShot;
    
    void Start()
    {
        _startPos = transform.localPosition;
    }
    
    void Update()
    {
        if (!_pv.IsMine)
            return;
        
        ShootTimer();
        
        if (Input.GetKey(KeyCode.Mouse0))
            Shoot();
    }
    
    private void Shoot()
    {
        if (_timeToNextShot > 0)
            return;
        
        Vector3 mousePosition = GameManager.Instance.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = (mousePosition - GameManager.Instance.Player.transform.position).normalized;
        
        var bullet = 
        Instantiate(_bulletPrefab, _gunPoint.position, _gunPoint.rotation);

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        bulletScript.BulletSpeed *= GameManager.Instance.PlayerStats.GetStatAmount(PlayerStats.StatTypes.ShootSpeed);
        bulletScript.Damage = _damage * GameManager.Instance.PlayerStats.GetStatAmount(PlayerStats.StatTypes.Damage);
        bullet.GetComponent<Rigidbody2D>().velocity += GameManager.Instance.Player.GetComponentInChildren<Rigidbody2D>().velocity / 2;

        _timeToNextShot = _fireRate;
    }

    private void ShootTimer()
    {
        if (_timeToNextShot > 0)
            _timeToNextShot -= Time.deltaTime;
    }
}
