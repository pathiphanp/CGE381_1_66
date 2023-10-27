using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frog : Platfrom
{
    [SerializeField] bool canJump;
    [SerializeField] float delayJump;
    [SerializeField] float pownJump;

    Animator anim;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PlatfromMove());

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override IEnumerator PlatfromMove()
    {
        return base.PlatfromMove();
    }



}
