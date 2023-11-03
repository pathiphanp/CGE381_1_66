using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ClockEnemy : Platfrom
{
    [SerializeField] GameObject head;
    Collider2D call;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = startPosition.transform.localPosition;
        target = endPosition.transform.localPosition;
        call = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
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
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
