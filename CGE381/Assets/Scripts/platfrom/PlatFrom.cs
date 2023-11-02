using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Platfrom : MonoBehaviour, DeleteChild, AddChild
{
    [SerializeField] bool platfrom = true;
    [Header("SetDelay")]
    [SerializeField] float delayStopPlatfrom;

    [Header("SetPosition")]
    [SerializeField] public GameObject startPosition;
    [SerializeField] public GameObject endPosition;
    [SerializeField] public Vector3 target;
    [Header("SetSpeed")]
    [SerializeField] float speedMove;
    [SerializeField] float timeDuration;

    void Start()
    {
        transform.localPosition = startPosition.transform.localPosition;
        target = endPosition.transform.localPosition;
    }
    void Update()
    {
        PlatfromMove();
    }
    public virtual void PlatfromMove()
    {
        if (transform.localPosition == startPosition.transform.localPosition)
        {
            speedMove = 0;
            target = endPosition.transform.localPosition;
        }
        else if (transform.localPosition == endPosition.transform.localPosition)
        {
            speedMove = 0;
            target = startPosition.transform.localPosition;
        }
        speedMove += Time.deltaTime;
        float percentCompete = speedMove / timeDuration;
        transform.localPosition =
        Vector3.MoveTowards(transform.localPosition,
        target, percentCompete);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Start")
        {
            speedMove = 0;
            target = endPosition.transform.localPosition;
        }
        if (other.gameObject.tag == "End")
        {
            speedMove = 0;
            target = startPosition.transform.localPosition;
        }*/

    }
    public void DeleteChild()
    {
        transform.DetachChildren();
    }

    public void AddChild(GameObject child)
    {
        if (platfrom)
        {
            child.transform.parent = transform;
        }
    }
}
