using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Platfrom
{
    [SerializeField] GameObject[] item;
    Animator anim;
    Collider2D coll;
    [HideInInspector] public BossClock bossClock;
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
        bossClock.canSpawn = true;
        coll.enabled = false;
        anim.Play("Die");
    }
}
