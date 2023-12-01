using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStar : MonoBehaviour
{
    Animator anim;
    Collider2D coll;
    AudioSource sfxSource;
    void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    public void Die()
    {
        coll.enabled = false;
        anim.Play("Die");
        sfxSource.PlayOneShot(SoundManager.Instance.SearchSfx("StarDie"));
    }
    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
