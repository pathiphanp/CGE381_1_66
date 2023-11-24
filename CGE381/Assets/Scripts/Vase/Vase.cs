using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    [SerializeField] GameObject cutScenesVase;
    bool canDie = false;
    private void OnEnable()
    {
        SpawnCutScenes.EndCutScene += Die;
    }
    private void OnDisable()
    {
        SpawnCutScenes.EndCutScene -= Die;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canDie = true;
            cutScenesVase.SetActive(true);
        }
    }

    void Die()
    {
        if (canDie)
        {
            Destroy(this.gameObject);
        }
    }
}
