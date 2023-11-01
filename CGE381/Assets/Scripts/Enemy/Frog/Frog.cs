using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    NONE, HUNT
}
public class Frog : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] public GameObject startPosition;
    [SerializeField] public GameObject endPosition;
    [SerializeField] public GameObject targetPosition;
    [SerializeField] public GameObject dataTargetPosition;
    [SerializeField] float delayHunt;
    [SerializeField] public Vector3 target;
    [SerializeField] public State state;

    [Header("Check")]
    [SerializeField] Vector2 top;
    [SerializeField] Vector2 bot;
    [SerializeField] float boxleft;
    [SerializeField] float boxRight;
    [SerializeField] float boxTop;
    [SerializeField] float botDown;
    [SerializeField] LayerMask ground;
    [Header("Jump")]
    [SerializeField] bool canJump;
    [SerializeField] float hightJump;
    [SerializeField] float distanceJump;
    float left;
    float right;
    [SerializeField] GameObject takedamage;


    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        left = -distanceJump;
        right = +distanceJump;
        target = endPosition.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Behavior();
        CheckGround();
    }
    void CheckGround()
    {
        top = new Vector2(transform.position.x - boxRight, transform.position.y - boxTop);
        bot = new Vector2(transform.position.x - boxleft, transform.position.y - botDown);
        if (Physics2D.OverlapArea(top, bot, ground) && canJump)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            anim.Play("JumpDown");
            if (canJump)
            {
                canJump = false;
                StartCoroutine(LoopJump());
            }
        }

    }
    IEnumerator LoopJump()
    {
        yield return new WaitForSeconds(1);
        anim.Play("JumpUp");
    }
    void Behavior()
    {
        if (takedamage == null)//Check Die
        {
            anim.Play("Die");
        }
        if (dataTargetPosition != null && state == State.HUNT)//HuntPlayer
        {
            targetPosition.transform.position = dataTargetPosition.transform.position;
            target = targetPosition.transform.localPosition;
        }
    }

    public IEnumerator Jump()
    {
        if (state == State.NONE)
        {
            if (transform.localPosition.x > startPosition.transform.localPosition.x)
            {
                target = endPosition.transform.localPosition;
            }
            else if (transform.localPosition.x < endPosition.transform.localPosition.x)
            {
                target = startPosition.transform.localPosition;
            }
        }
        if (transform.localPosition.x < target.x)
        {
            //Right
            sprite.flipX = true;
            distanceJump = right;
        }
        else
        {
            //Left
            sprite.flipX = false;
            distanceJump = left;
        }
        rb.gravityScale = 1;
        rb.velocity = new Vector2(rb.velocity.x, hightJump);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(distanceJump, rb.velocity.y);
        //Debug.Log("Up : " + rb.velocity.y);
    }
    public IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(1);
        canJump = true;
    }

    public IEnumerator ResetHunt()
    {
        yield return new WaitForSeconds(delayHunt);
        state = State.NONE;
        if (state == State.HUNT)
        {
            yield break;
        }
        dataTargetPosition = null;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void OnDrawGizmos()
    {
        top = new Vector2(transform.position.x - boxRight, transform.position.y - boxTop);
        bot = new Vector2(transform.position.x - boxleft, transform.position.y - botDown);
        DebugBox.DrawRectange(top, bot);
    }
}
