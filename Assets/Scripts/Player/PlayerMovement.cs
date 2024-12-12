using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Referances")] 
    
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Transform _crosshairSprite;
    
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _cameras;
    [SerializeField] private GameObject _ui;
    [SerializeField] private ParticleSystem _landing_JumpingParticles;

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
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (base.IsOwner)
        {
            _playerManager.Camera = _cameras.GetComponentInChildren<Camera>();
            _cameras.transform.parent = null;
        }

        else
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            //gameObject.GetComponent<WeaponHolsterScript>().enabled = false;
            
            Destroy(_rb);
            Destroy(_cameras);
        }
    }
    
    void Start()
    {
        Cursor.visible = false;
        
        _initalSpeed = _playerStats.GetStatAmount(PlayerStats.StatTypes.Speed);
    }

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && _rb.linearVelocity.y > 0.0f)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * 0.5f);
        }

        Vector2 mousePos = GameManager.Instance.MainCam.ScreenToWorldPoint(Input.mousePosition);
        _crosshairSprite.transform.position = mousePos;
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector2(_horizontalInput * _playerStats.GetStatAmount(PlayerStats.StatTypes.Speed), 0.0f), ForceMode2D.Force);

        if (IsGrounded() && Mathf.Approximately(_horizontalInput, 0.0f))
        {
            float counterForce = -_rb.linearVelocity.x * _counterMovement;
            _rb.AddForce(new Vector2(counterForce, 0.0f), ForceMode2D.Force);
        }

        if (Mathf.Abs(_rb.linearVelocity.x) > _playerStats.GetStatAmount(PlayerStats.StatTypes.MaxSpeed))
        {
            _rb.linearVelocity = new Vector2(Mathf.Sign(_rb.linearVelocity.x) * _playerStats.GetStatAmount(PlayerStats.StatTypes.MaxSpeed), _rb.linearVelocity.y);
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
}