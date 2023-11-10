using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D call;
    Animator anim;
    [HideInInspector] public SpawnEnemyDown control;
    [SerializeField] public float speedDown;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        call = this.AddComponent<PolygonCollider2D>();
        rb = this.AddComponent<Rigidbody2D>();
        call.isTrigger = true;
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
            anim.Play("Die");
        }
        if(other.gameObject.tag == "Ground")
        {
            Invoke("DelayDie", 2f);
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


}
