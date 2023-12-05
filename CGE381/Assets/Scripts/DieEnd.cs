using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEnd : MonoBehaviour
{
    [SerializeField] GameObject die;
    void Die()
    {
        Destroy(die);
    }

    void SoundDie()
    {
        SoundManager.Instance.PlaySfx("EnemyDie");
    }
}
