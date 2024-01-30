using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referances")] 
    [SerializeField] private PhotonView _pv;
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _camera;
    
    [Header("Values")] 
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _staminaDrainTime;

    private float _stamina;
    private float _horizontalInput;
    private float _playerYVelocity;

    private float _initalSpeed;
    
    void Start()
    {
        if (!_pv.IsMine)
        {
            Destroy(_camera);
            Destroy(_rb);
        }

        _stamina = _maxStamina;
        _initalSpeed = _speed;
    }
    
    void Update()
    {
        if (!_pv.IsMine)
            return;
        
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && _rb.velocity.y > 0.0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }

        if (Input.GetKey(KeyCode.LeftShift) && _stamina > 0.0f)
        {
            _stamina -= Time.deltaTime / _staminaDrainTime;
        }
    }

    private void FixedUpdate()
    {
        if (!_pv.IsMine)
            return;
        _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
