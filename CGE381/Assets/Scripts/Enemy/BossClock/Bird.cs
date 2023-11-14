using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Platfrom
{
    [SerializeField] GameObject[] item;
    Animator anim;
    Collider2D coll;
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    public void DropItem()
    {
        Die();
        Instantiate(item[Random.Range(0, item.Length)]);
    }
    void Die()
    {
        coll.enabled = false;
        anim.Play("Die");
    }
}
