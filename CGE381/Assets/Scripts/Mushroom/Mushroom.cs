using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Collider2D mushroom;
    Animator anim;
    [SerializeField] float cooldown;
    bool canGain = true;

    void Start()
    {
        mushroom = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && canGain)
        {
            other.gameObject.GetComponent<Player>().AliceChangeSize();
            canGain = false;
            anim.Play("PlaAtk");
        }
    }
    public void ResetSetMushroom()
    {
        canGain = true;
    }
}
