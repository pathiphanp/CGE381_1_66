using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Animator anim;
    Collider2D coll;
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    public void Die()
    {
        coll.enabled = false;
        anim.Play("Die");
    }
    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
