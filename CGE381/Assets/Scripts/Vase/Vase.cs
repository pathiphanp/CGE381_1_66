using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject cutScenesVase;
    bool canDie = false;
    private void OnEnable()
    {
        SpawnCutScenes.EndCutScene += Die;
    }
    private void OnDisable()
    {
        SpawnCutScenes.EndCutScene -= Die;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canDie = true;
            anim.Play("idel");
            SoundManager.Instance.PlaySfx("StarDie");
            cutScenesVase.SetActive(true);
        }
    }

    void Die()
    {
        if (canDie)
        {
            Destroy(this.gameObject);
        }
    }
}
