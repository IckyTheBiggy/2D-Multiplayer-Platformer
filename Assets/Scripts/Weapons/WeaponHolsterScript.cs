using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolsterScript : MonoBehaviour
{
    [SerializeField] private GameObject _weaponHolsterObject;
    [SerializeField] private Camera _playerCamera;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        Vector3 mousePos = _playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        _weaponHolsterObject.transform.rotation = rotation;
    }
}
