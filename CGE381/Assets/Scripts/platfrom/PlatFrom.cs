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
    bool canMove = true;
    [Header("SetSpeed")]
    [SerializeField] float speedMove;
    [SerializeField] public float stop;

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
        if (transform.localPosition == target && canMove)
        {
            canMove = false;
            StartCoroutine(DelayPlatFrom());
        }
        transform.localPosition =
        Vector3.MoveTowards(transform.localPosition,
        target, speedMove * Time.deltaTime * stop);
    }
    IEnumerator DelayPlatFrom()
    {
        yield return new WaitForSeconds(delayStopPlatfrom);
        if (transform.localPosition == startPosition.transform.localPosition)
        {
            target = endPosition.transform.localPosition;
        }
        else if (transform.localPosition == endPosition.transform.localPosition)
        {
            target = startPosition.transform.localPosition;
        }
        canMove = true;
    }
    public void DeleteChild()
    {
        transform.DetachChildren();
    }

    public void AddChild(GameObject child)
    {
        if (platfrom)
        {
            //Add in Platfrom
            child.transform.parent = transform;
            //Set Position on platfrom
            Vector3 a = new Vector3(child.transform.localPosition.x,
            0.53f, child.transform.localPosition.z);
            if (child.transform.localPosition != a)
            {
                child.transform.localPosition = a;
            }
        }
    }

    void ResetLayerPlatfrom()
    {
        gameObject.layer = 11 << 0;
    }
}
