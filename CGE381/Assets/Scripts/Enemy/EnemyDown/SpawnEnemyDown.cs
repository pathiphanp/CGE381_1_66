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
    [SerializeField] float delayObjDie;
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
        GameObject enemy = Instantiate(objEnemy[Random.Range(0, objEnemy.Length)],
        transform.localPosition, transform.localRotation);
        ObjEnemy _enemy = enemy.GetComponent<ObjEnemy>();
        _enemy.control = this;
        _enemy.delayDie = delayObjDie;
        yield return new WaitForSeconds(delaySpawn);
        canSpawn = true;
    }
}
