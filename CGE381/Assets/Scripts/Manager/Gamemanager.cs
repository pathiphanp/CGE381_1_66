using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : Singletons<Gamemanager>
{
    public static event Action PlayerUIMode;
    public static event Action PlayerMode;
    public bool cutScenesStartGame;

    public static void ChangeUIMode()
    {
        PlayerUIMode?.Invoke();
    }
    public static void ChangePlayerMode()
    {
        PlayerMode?.Invoke();
    }

    public void NextScenes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
