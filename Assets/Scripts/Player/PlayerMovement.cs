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
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _cameras;
    [SerializeField] private GameObject _ui;

    [Header("Values")] 
    [SerializeField] private float _counterMovement;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _staminaDrainTime;

    private float _horizontalInput;
    
    private float _stamina;
    private float _playerYVelocity;
    private bool _isFacingRight;

    private float _initalSpeed;
    
    void Start()
    {
        if (!_pv.IsMine)
        {
            Destroy(_cameras);
            Destroy(_rb);
            Destroy(_ui);
        }
        
        _initalSpeed = _playerStats.Speed;
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
    }

    private void FixedUpdate()
    {
        if (!_pv.IsMine)
            return;
        _rb.AddForce(new Vector2(_horizontalInput * _playerStats.Speed, 0.0f), ForceMode2D.Force);

        if (IsGrounded() && Mathf.Approximately(_horizontalInput, 0.0f))
        {
            float counterForce = -_rb.velocity.x * _counterMovement;
            _rb.AddForce(new Vector2(counterForce, 0.0f), ForceMode2D.Force);
        }

        if (Mathf.Abs(_rb.velocity.x) > _playerStats.MaxSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _playerStats.MaxSpeed, _rb.velocity.y);
        }

        if (_horizontalInput > 0 || _horizontalInput < 0)
        {
            TurnCheck();
        }
    }

    private void TurnCheck()
    {
        if (_horizontalInput > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
            Turn();
        }
        
        else if (_horizontalInput < 0 && _isFacingRight)
        {
            _isFacingRight = false;
            Turn();
        }
    }

    private void Turn()
    {
        if (_isFacingRight)
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180.0f, transform.rotation.z);
        else
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0.0f, transform.rotation.z);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}