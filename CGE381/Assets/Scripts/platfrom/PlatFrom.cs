using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platfrom : MonoBehaviour, DeleteChild, AddChild
{
    [Header("SetDelay")]
    [SerializeField] float delayStopPlatfrom;

    [Header("SetPosition")]
    [SerializeField] GameObject startPosition;
    [SerializeField] GameObject endPosition;
    [Header("SetSpeed")]
    [SerializeField] float speedMove;
    [SerializeField] float timeDuration;

    float i1 = 0;
    float i2 = 0;

    void Start()
    {
        transform.localPosition = startPosition.transform.localPosition;
        StartCoroutine(PlatfromMove());
    }
    void Update()
    {

    }
    IEnumerator PlatfromMove()
    {
        Vector3 target;
        speedMove = 0;
        if (transform.localPosition == endPosition.transform.localPosition)
        {
            target = startPosition.transform.localPosition;
        }
        else
        {
            target = endPosition.transform.localPosition;
        }

        while (transform.localPosition != target)
        {
            speedMove += Time.deltaTime;
            //Debug.Log("in : " + (i2 += Time.deltaTime));
            float percentCompete = speedMove / timeDuration;
            transform.localPosition =
            Vector3.Lerp(transform.localPosition,
            target, percentCompete);
            yield return true;
        }
        yield return new WaitForSeconds(delayStopPlatfrom);
        StartCoroutine(PlatfromMove());
    }

    public void DeleteChild()
    {
        transform.DetachChildren();
    }

    public void AddChild(GameObject child)
    {
        child.transform.parent = transform;
    }
}
