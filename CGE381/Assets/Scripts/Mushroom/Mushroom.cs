using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Collider2D mushroom;
    [SerializeField] float cooldown;

    void Start()
    {
        mushroom = GetComponent<Collider2D>();
        StartCoroutine(ResetSetMushroom());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            StartCoroutine(ResetSetMushroom());
        }
    }

    IEnumerator ResetSetMushroom()
    {
        mushroom.enabled = false;
        yield return new WaitForSeconds(cooldown);
        mushroom.enabled = true;

    }
}
