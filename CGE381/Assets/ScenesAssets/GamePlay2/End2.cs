using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End2 : MonoBehaviour
{
    [SerializeField] GameObject bgBlack;
    [SerializeField] GameObject CutScenes;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            StartCoroutine(End());
            Gamemanager.ChangeUIMode();
        }
    }

    IEnumerator End()
    {
        bgBlack.SetActive(true);
        yield return new WaitForSeconds(1f);
        bgBlack.SetActive(false);
        CutScenes.SetActive(true);
        NextMap();
    }

    public void NextMap()
    {
        Gamemanager.Instance.NextScenes();
    }
}
