using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    NONE, HUNT
}
public class EnemyJump : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] public GameObject startPosition;
    [SerializeField] public GameObject endPosition;
    [SerializeField] public GameObject targetPosition;
    [SerializeField] public GameObject dataTargetPosition;
    [SerializeField] float delayHunt;
    [SerializeField] public Vector3 target;
    [SerializeField] public State state;

    [Header("Ray Check Ground")]
    [SerializeField] GameObject checkGround;
    [SerializeField] Vector2 top;
    [SerializeField] Vector2 bot;
    [SerializeField] float boxleft;
    [SerializeField] float boxRight;
    [SerializeField] float boxTop;
    [SerializeField] float botDown;
    [SerializeField] LayerMask ground;
    [Header("Jump")]
    [SerializeField] float delayJump;
    [Header("Up")]
    [SerializeField] bool canJump;
    bool jump = true;
    float dataHight = 0;
    [SerializeField] float hightMax;
    [SerializeField] float speedToHight;
    [Header("Down")]
    [SerializeField] float gravityDown;
    [SerializeField] float speedDown;
    float dataSpeedDown;
    bool downJump;
    [Header("distanceJump")]
    [SerializeField] float distanceJump;
    float distanceleft;
    float distanceright;
    [Header("HeadEnemy")]
    [SerializeField] GameObject takedamage;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    AudioSource sfxSource;
    // Start is called before the first frame update
    void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        AllSetValue();
    }
    void AllSetValue()
    {
        distanceleft = -distanceJump;
        distanceright = +distanceJump;
        dataSpeedDown = speedDown;
        target = endPosition.transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        Behavior();
        CheckGround();
        JumpControl();
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
    void JumpControl()
    {
        if (canJump == true)
        {
            dataHight = Mathf.Lerp(dataHight, speedToHight, Time.deltaTime);
            rb.velocity = new Vector2(rb.velocity.x, dataHight);
            if (rb.velocity.y >= hightMax)
            {
                downJump = true;
                canJump = false;
                dataSpeedDown = speedDown;
                rb.velocity = new Vector2(rb.velocity.x, dataSpeedDown);
            }
        }
        if (rb.velocity.y < 0 && downJump)
        {
            downJump = false;
            rb.gravityScale = gravityDown;
        }
    }
    void CheckGround()
    {
        top = new Vector2(transform.position.x - boxRight, transform.position.y - boxTop);
        bot = new Vector2(transform.position.x - boxleft, transform.position.y - botDown);
        if (Physics2D.OverlapArea(top, bot, ground) && jump)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            dataSpeedDown = 0;
            dataHight = 0;
            if (jump)
            {
                jump = false;
                StartCoroutine(LoopJump());
            }
        }
    }
    IEnumerator LoopJump()
    {
        yield return new WaitForSeconds(delayJump);
        anim.Play("JumpUp");
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
            distanceJump = distanceright;
        }
        else
        {
            //Left
            sprite.flipX = false;
            distanceJump = distanceleft;
        }
        rb.gravityScale = 1;
        checkGround.SetActive(false);
        canJump = true;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(distanceJump, rb.velocity.y);

        //Debug.Log("Up : " + rb.velocity.y);
    }
    public IEnumerator ResetJump()
    {
        anim.Play("JumpDown");
        checkGround.SetActive(true);
        jump = true;
        yield return new WaitForSeconds(0);
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

    void SoundJumpFrog()
    {
        sfxSource.PlayOneShot(SoundManager.Instance.SearchSfx("EnemyJump"));
    }

    void SoundDie()
    {
        sfxSource.PlayOneShot(SoundManager.Instance.SearchSfx("EnemyDie"));
    }
}
