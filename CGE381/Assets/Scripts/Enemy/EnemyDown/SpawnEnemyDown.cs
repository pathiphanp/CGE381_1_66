using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemyDown : MonoBehaviour
{
    ObjEnemy _objEnemy;
    public GameObject[] objEnemy;
    [SerializeField] float delaySpawn;

    [Header("Spawn type")]
    [SerializeField] public bool spawnSide;
    [SerializeField] public bool spawnDown;
    bool canSpawn = true;

    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(SpawnObj());
        }
    }
    IEnumerator SpawnObj()
    {
        GameObject enemy = Instantiate(objEnemy[Random.Range(0, objEnemy.Length)], transform.localPosition, transform.localRotation);
        enemy.GetComponent<ObjEnemy>().control = this;
        yield return new WaitForSeconds(delaySpawn);
        canSpawn = true;
    }
}
