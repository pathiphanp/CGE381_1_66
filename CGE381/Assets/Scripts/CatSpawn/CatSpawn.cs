using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawn : MonoBehaviour
{
    Collider2D col;
    Animator anim;
    [SerializeField] GameObject spawnCutScenes;
    bool candie;

    private void OnEnable()
    {
        SpawnCutScenes.EndCutScene += OffWarp;
    }
    void OnDisable()
    {
        SpawnCutScenes.EndCutScene -= OffWarp;
    }
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.Play("CatWarp");
            candie = true;
            Gamemanager.ChangeUIMode();
        }
    }
    void PlayCutScenes()
    {
        col.enabled = false;
        spawnCutScenes.SetActive(true);
    }
    void OffWarp()
    {
        anim.Play("OffWarp");
    }
    void Die()
    {
        if (candie)
        {
            Destroy(this.gameObject);
        }
    }
    void SoundCatWarp()
    {
        SoundManager.Instance.PlaySfx("CatWarp");
    }
}
