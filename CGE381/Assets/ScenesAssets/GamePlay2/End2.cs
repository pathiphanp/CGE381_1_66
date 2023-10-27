using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End2 : MonoBehaviour
{
    [SerializeField] GameObject CutScenes;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            End();
        }
    }

    void End()
    {
        CutScenes.SetActive(true);
    }
}
