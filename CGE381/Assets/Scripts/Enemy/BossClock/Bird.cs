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
        coll.enabled = false;
        sprite.enabled = false;
        bossClock.canSpawn = true;
        dieAnimation.SetActive(true);
    }

    void SoundFly()
    {
        SoundManager.Instance.PlaySfx("Fly");
    }
    void SoundDie()
    {
        SoundManager.Instance.PlaySfx("Die");
    }
}
