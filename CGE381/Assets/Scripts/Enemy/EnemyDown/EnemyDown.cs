using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDown : MonoBehaviour
{
    public GameObject[] objEnemy;
    void Update()
    {

    }
    IEnumerator SpawnObj()
    {
        yield return new WaitForSeconds(3);
        GameObject enemy = Instantiate(objEnemy[Random.Range(0, objEnemy.Length)]);
        enemy.AddComponent<ObjEnemy>();
    }
}
