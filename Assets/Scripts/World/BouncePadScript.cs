using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadScript : MonoBehaviour
{
    [Header("Values")] 
    [SerializeField] private float _bounceForce;
    
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("Bullet"))
        {
            var rb =
            other.transform.GetComponent<Rigidbody2D>();

            rb.linearVelocity = Vector2.zero;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, _bounceForce);
        }
    }
}
