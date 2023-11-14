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
    public override void Start()
    {
        base.Start();
        SpawdHead();
    }
    public override void PlatfromMove()
    {
        base.PlatfromMove();
    }

    public override void CheckDie()
    {
        if (head.gameObject == null && !immrotal)
        {
            immrotal = true;
            stop = 0;
            if (canSpawn)
            {
                canSpawn = false;
                SpawnBird();
            }
            anim.Play("Stun");
        }
    }

    IEnumerator DelayGainDamage()
    {
        hp--;
        if (hp > 0)
        {
            yield return new WaitForSeconds(1);
            SpawdHead();
            immrotal = false;
            StartCoroutine(delaySpeed());
        }
        else
        {
            Die();
        }
    }
    IEnumerator delaySpeed()
    {
        stop = 2;
        yield return new WaitForSeconds(2f);
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
        if (transform.localPosition.x > target.x)//Left
        {
            target = startPosition.transform.localPosition;
        }
        else//Right
        {
            target = endPosition.transform.localPosition;
        }
        bird.transform.position = new Vector3(bossset.transform.position.x,
            prefabbrid.transform.localPosition.y, prefabbrid.transform.localPosition.z);
        Bird _bird = null;
        for (int i = 0; i < bird.transform.childCount; i++)
        {
            if (bird.transform.GetChild(i).GetComponent<Bird>())
            {
                _bird = bird.transform.GetChild(i).GetComponent<Bird>();
            }
            else
            {

            }
        }
        _bird.startPosition.transform.localPosition = startPosition.transform.localPosition;
        _bird.endPosition.transform.localPosition = endPosition.transform.localPosition;
        _bird.target = target;

    }
    void Die()
    {
        coll.enabled = false;
        anim.Play("Die");
    }
    void Destroythis()
    {
        Destroy(this.gameObject);
    }

}
