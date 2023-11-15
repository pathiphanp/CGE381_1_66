using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Platfrom
{
    [SerializeField] GameObject[] item;
    Animator anim;
    Collider2D coll;
    public BossClock bossClock;
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        SetUp();
    }
    public void DropItem()
    {
        Die();
        Instantiate(item[Random.Range(0, item.Length)]);
    }
    public void SetUp()
    {
        transform.localPosition = startPosition.transform.localPosition;
    }
    void Die()
    {
        bossClock.canSpawn = true;
        coll.enabled = false;
        anim.Play("Die");
    }
}
