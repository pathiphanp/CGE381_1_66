using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dotdestroy : Singletons<Dotdestroy>
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
        
    }


}
