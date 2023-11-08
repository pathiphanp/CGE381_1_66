using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGainAllStars : Singletons<CheckGainAllStars>
{
    [SerializeField] GameObject cutScenes;
    int countStar;
    int stargain;
    public void CheckStar()
    {
        stargain++;
        if (stargain == countStar)
        {
            cutScenes.SetActive(true);
        }
    }
}
