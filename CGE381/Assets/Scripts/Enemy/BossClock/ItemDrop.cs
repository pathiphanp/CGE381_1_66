using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] String mytag;
    Collider2D coll;
    Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            this.gameObject.tag = mytag;
        }
    }
}
