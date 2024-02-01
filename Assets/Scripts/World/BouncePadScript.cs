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
        if (other.transform.CompareTag("Player"))
        {
            var rb =
            other.transform.GetComponent<Rigidbody2D>();

            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, _bounceForce);
        }
    }
}
