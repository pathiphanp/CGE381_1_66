using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModeGame : MonoBehaviour
{
    [SerializeField] GameObject allHeal;
    void Start()
    {
        if (SaveManager.Instance.modeGame == ModeGame.EASY)
        {
            allHeal.SetActive(true);
        }
        else
        {
            allHeal.SetActive(false);
        }
    }
}
