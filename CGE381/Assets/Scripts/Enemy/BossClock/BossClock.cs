using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClock : ClockEnemy
{
    [SerializeField] int hp;
    [SerializeField] GameObject prefabhead;
    [SerializeField] bool immrotal;
    [SerializeField] GameObject prefabbrid;
    [SerializeField] GameObject bossset;
    [HideInInspector] public bool canSpawn = true;
    Bird _bird = null;
    [SerializeField] GameObject cutScenesEnd;
    public override void Start()
    {
        base.Start();
        SpawdHead();
    }

    public override void CheckDie()
    {
        if (head.gameObject == null && !immrotal)
        {
            immrotal = true;
            hp--;
            stop = 0;
            if (hp > 0)
            {
                if (canSpawn)
                {
                    SpawnBird();
                }
                anim.Play("Stun");
            }
            else
            {
                Die();
            }
        }
    }

    IEnumerator DelayGainDamage()
    {
        StartCoroutine(delaySpeed());
        yield return new WaitForSeconds(0.1f);
        SpawdHead();
        immrotal = false;
    }
    IEnumerator delaySpeed()
    {
        stop = 1;
        yield return new WaitForSeconds(1f);
        stop = 1;
    }
    void SpawdHead()
    {
        Vector3 pointHead = new Vector3(0, 1.5f, 0);
        GameObject newhead = Instantiate(prefabhead.gameObject, transform);
        newhead.transform.localPosition = pointHead;
        head = newhead;
    }
    void SpawnBird()
    {
        Vector3 target = Vector3.zero;
        GameObject bird = Instantiate(prefabbrid);

        bird.transform.position = new Vector3(bossset.transform.position.x,
            prefabbrid.transform.localPosition.y, prefabbrid.transform.localPosition.z);
        for (int i = 0; i < bird.transform.childCount; i++)
        {
            if (bird.transform.GetChild(i).GetComponent<Bird>())
            {
                _bird = bird.transform.GetChild(i).GetComponent<Bird>();
                _bird.bossClock = this;
                _bird.startPosition.transform.localPosition = startPosition.transform.localPosition;
                _bird.endPosition.transform.localPosition = endPosition.transform.localPosition;
                _bird.transform.localPosition = new Vector3(10, 0, 0);
                if (transform.localPosition.x > target.x)//Left
                {
                    target = startPosition.transform.localPosition;
                    _bird.transform.localPosition = endPosition.transform.localPosition;

                }
                else//Right
                {
                    target = endPosition.transform.localPosition;
                    _bird.transform.localPosition = startPosition.transform.localPosition;
                }
                _bird.target = target;
            }
            else
            {

            }
        }

    }
    void Die()
    {
        coll.enabled = false;
        stop = 0;
        anim.Play("Die");
    }
    public override void Destroy()
    {
        cutScenesEnd.SetActive(true);
        base.Destroy();
    }

    void SoundStun()
    {
        sfxSound.PlayOneShot(SoundManager.Instance.SearchSfx("EnemyStun"));
    }
}
