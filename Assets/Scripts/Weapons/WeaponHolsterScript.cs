using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolsterScript : MonoBehaviour
{
    [SerializeField] private GameObject _weaponHolsterObject;
    [SerializeField] private GameObject _weaponSlot;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        Vector3 mousePos = GameManager.Instance.MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (angle > 85.0f || angle < -85.0f)
            _weaponSlot.transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);
        else
            _weaponSlot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        _weaponHolsterObject.transform.rotation = rotation;
    }
}
