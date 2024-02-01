using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private PhotonView _pv;
    
    [SerializeField] private float _fireRate;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletPrefab;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (!_pv.IsMine)
            return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot()
    {
        Vector3 mousePosition = GameManager.Instance.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = (mousePosition - GameManager.Instance.Player.transform.position).normalized;
        
        var bullet = 
        PhotonNetwork.Instantiate(_bulletPrefab.name, _gunPoint.position, _gunPoint.rotation);

        bullet.GetComponent<BulletScript>().Damage = _damage;
    }
}
