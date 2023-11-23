using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockEnemy : Platfrom
{
    [SerializeField] public GameObject head;
    [SerializeField] bool havehead = true;
    [HideInInspector] public Collider2D coll;
    [HideInInspector] public Animator anim;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    public virtual void Start()
    {
        transform.localPosition = startPosition.transform.localPosition;
        target = endPosition.transform.localPosition;
        coll = GetComponent<Collider2D>();
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
        CheckDie();
        base.PlatfromMove();
        if (transform.localPosition.x > target.x)//Left
        {
            sprite.flipX = true;
        }
        else//Right
        {
            sprite.flipX = false;
        }
    }
    public virtual void CheckDie()
    {
        if (head.gameObject == null && havehead)
        {
            target = transform.localPosition;
            coll.enabled = false;
            anim.Play("Die");
        }
    }
    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }
}
