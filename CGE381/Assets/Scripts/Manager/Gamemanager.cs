using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : Singletons<Gamemanager>
{
    public static event Action PlayerUIMode;
    public static event Action PlayerMode;

    public static void ChangeUIMode()
    {
        PlayerUIMode?.Invoke();
    }
    public static void ChangePlayerMode()
    {
        PlayerUIMode?.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
