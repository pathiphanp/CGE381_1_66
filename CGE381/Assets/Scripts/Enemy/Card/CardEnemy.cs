using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEnemy : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    [SerializeField] float targetY;

    float speed;
    [SerializeField] float duration;

    [SerializeField] float delayAtk;


    [SerializeField] GameObject weapon;

    void Start()
    {
        targetY = end.transform.localPosition.y;
        StartCoroutine(LoopAtk());
    }
    IEnumerator LoopAtk()
    {
        while (weapon.transform.localPosition.y != targetY)
        {
            speed += Time.deltaTime;
            float percencomplete = speed / duration;
            float moveY = Mathf.Lerp(weapon.transform.localPosition.y,
            targetY, percencomplete);
            weapon.transform.localPosition = new Vector3(weapon.transform.localPosition.x,
            moveY, weapon.transform.localPosition.z);
            yield return true;
        }
        if (weapon.transform.localPosition.y == start.transform.localPosition.y)
        {
            targetY = end.transform.localPosition.y;
        }
        else
        {
            targetY = start.transform.localPosition.y;
        }
        yield return new WaitForSeconds(delayAtk);
        speed = 0;
        StartCoroutine(LoopAtk());
    }
}
