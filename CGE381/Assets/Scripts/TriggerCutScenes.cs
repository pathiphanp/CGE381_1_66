using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutScenes : MonoBehaviour
{
    private void OnEnable()
    {
        SpawnCutScenes.EndCutScene += DestroyCutScenes;
    }
    void OnDisable()
    {
        SpawnCutScenes.EndCutScene -= DestroyCutScenes;
    }
    [SerializeField] GameObject cutScenes;
    void OnTriggerEnter2D(Collider2D other)
    {
        cutScenes.SetActive(true);
        Destroy(this.gameObject);
    }

    void DestroyCutScenes()
    {
        Destroy(this.gameObject);
    }
}
