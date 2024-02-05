using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class KnockBackBlasterScript : MonoBehaviour
{
    [SerializeField] private float _knockBackForce;

    private PhotonView _pv;
    private Camera _playerCam;
    private Transform _playerTransform;
    private Rigidbody2D _rb;

    private void Start()
    {
        _pv = GameManager.Instance.Player.GetComponentInChildren<PhotonView>();

        _playerCam = GameManager.Instance.MainCam;
        _playerTransform = GameManager.Instance.Player.transform;
        _rb = GameManager.Instance.Player.GetComponentInChildren<Rigidbody2D>();
        
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
        Vector3 mousePosition = _playerCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = (mousePosition - _playerTransform.position).normalized;

        _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
        
        _rb.AddForce(shootDirection * -_knockBackForce, ForceMode2D.Impulse);
    }
}
