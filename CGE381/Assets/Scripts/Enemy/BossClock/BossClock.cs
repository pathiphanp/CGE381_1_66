using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClock : ClockEnemy
{
    [SerializeField] int hp;
    [SerializeField] GameObject prefabhead;
    [SerializeField] bool immrotal;
    [SerializeField] GameObject prefabbrid;
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
            SpawnBird();
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
            stop = 1;
        }
        else
        {
            Die();
        }
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
        GameObject bird = Instantiate(prefabbrid);
        Bird _bird = bird.GetComponent<Bird>();
        _bird.startPosition = startPosition;
        _bird.endPosition = endPosition;
        Vector3 target = Vector3.zero;
        if (transform.localPosition.x > target.x)//Left
        {
            target = startPosition.transform.position;
        }
        else//Right
        {
            target = endPosition.transform.position;
        }
        _bird.target = target;
        bird.transform.position = new Vector3(target.x,
            prefabbrid.transform.position.y, prefabbrid.transform.position.z);
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
