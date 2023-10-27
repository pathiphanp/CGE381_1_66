using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] Collider2D checkGround;
    [SerializeField] float radius;
    [SerializeField] LayerMask ground;

    [SerializeField] bool canJump;
    [SerializeField] float pownJump;

    Animator anim;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LoopJump();

    }
    void Behavior()
    {
        //FindPlayer
        
        //HuntPlayer

    }
    void LoopJump()
    {
        if (Physics2D.OverlapCircle(checkGround.transform.position, radius, ground) && canJump)
        {
            canJump = false;
            anim.Play("Jump");
        }
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, pownJump);
    }
    public void ResetJump()
    {
        canJump = true;
    }


}
