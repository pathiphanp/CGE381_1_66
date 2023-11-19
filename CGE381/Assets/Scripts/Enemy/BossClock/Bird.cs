using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Platfrom
{
    [SerializeField] GameObject[] item;
    [SerializeField] GameObject dieAnimation;
    Collider2D coll;
    SpriteRenderer sprite;
    public BossClock bossClock;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }
    public void DropItem()
    {
        Die();
        Instantiate(item[Random.Range(0, item.Length)], transform.position, transform.rotation);
    }
    void Die()
    {
        bossClock.canSpawn = true;
        coll.enabled = false;
        sprite.enabled = false;
        dieAnimation.SetActive(true);
    }
}
