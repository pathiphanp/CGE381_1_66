using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Collider2D mushroom;
    Animator anim;
    [SerializeField] float cooldown;

    void Start()
    {
        mushroom = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            mushroom.enabled = false;
            anim.Play("PlaAtk");
        }
    }

    public void ResetSetMushroom()
    {
        Debug.Log("Reset");
        mushroom.enabled = true;
    }
}
