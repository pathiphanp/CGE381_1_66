using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFromTrick : MonoBehaviour
{
    Animator anim;
    public float waitStartTick;
    bool canAction;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FloorTrick());
        }
    }
    IEnumerator FloorTrick()
    {
        canAction = true;
        yield return new WaitForSeconds(waitStartTick);
        if (canAction)
        {
            anim.Play("OpenTrick");
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            canAction = false;
        }
    }

    IEnumerator ResetTrick()
    {
        yield return new WaitForSeconds(3f);
        anim.Play("ResetTrick");
    }
}
