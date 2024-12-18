using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackBlasterScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    [SerializeField] private float _knockBackForce;
    
    private Camera _playerCam;
    private Transform _playerTransform;
    private Rigidbody2D _rb;

    private void Start()
    {
        _playerTransform = _player.transform;
        _rb = _player.GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot()
    {
        Vector3 mousePosition = _playerCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = (mousePosition - _playerTransform.position).normalized;

        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0.0f);
        
        _rb.AddForce(shootDirection * -_knockBackForce, ForceMode2D.Impulse);
    }
}
