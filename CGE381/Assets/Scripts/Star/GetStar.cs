using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStar : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Die()
    {
        anim.Play("Die");
    }
    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
