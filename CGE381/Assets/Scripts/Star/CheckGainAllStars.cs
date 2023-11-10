using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGainAllStars : Singletons<CheckGainAllStars>
{

    [SerializeField] GameObject cutScenes;
    [SerializeField] int countStar;
    [SerializeField] int stargain;
    void Start()
    {
        countStar = transform.childCount - 1;
    }
    public void CheckStar()
    {
        stargain++;
        if (stargain == countStar)
        {
            cutScenes.SetActive(true);
            Player.GainHealAll();
        }
    }
}
