using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;
    public float delayDie;
    /*[HideInInspector]*/ public SpawnEnemyDown control;
    [SerializeField] public float speedDown;
    AudioSource sfxSound;
    [SerializeField] LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        sfxSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        coll = this.AddComponent<PolygonCollider2D>();
        rb = this.AddComponent<Rigidbody2D>();
        coll.isTrigger = true;
        rb.gravityScale = 0;
        if (control != null)
        {
            if (control.spawnSide)
            {
                rb.velocity = new Vector2(speedDown, rb.velocity.y);
            }
            if (control.spawnDown)
            {
                rb.velocity = new Vector2(rb.velocity.x, speedDown);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            coll.enabled = false;
            anim.Play("Die");
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            coll.enabled = false;
            Invoke("DelayDie", delayDie);
        }
    }

    void Destroythis()
    {
        Destroy(this.gameObject);
    }

    void DelayDie()
    {
        anim.Play("Die");
    }

    void SoundDie()
    {
        sfxSound.PlayOneShot(SoundManager.Instance.SearchSfx("GlassDie"));
    }

    void SoundRoll()
    {

    }

}
