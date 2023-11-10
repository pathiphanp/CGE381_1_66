using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ClockEnemy : Platfrom
{
    [SerializeField] GameObject head;
    Collider2D call;
    Animator anim;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = startPosition.transform.localPosition;
        target = endPosition.transform.localPosition;
        call = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlatfromMove();
    }
    public override void PlatfromMove()
    {
        if (head.gameObject == null)
        {
            target = transform.localPosition;
            call.enabled = false;
            anim.Play("Die");
        }
        base.PlatfromMove();
        if (transform.localPosition.x > target.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
