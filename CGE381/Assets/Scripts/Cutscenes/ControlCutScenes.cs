using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ControlCutScenes : MonoBehaviour
{
    public SpawnCutScenes spawnCutScenes;
    public PlayableDirector director;
    private void OnEnable()
    {
        Player.CutSceneTrigger += SkipCutScenes;
    }
    private void OnDisable()
    {
        Player.CutSceneTrigger -= SkipCutScenes;
    }
    void Start()
    {
        Gamemanager.ChangeUIMode();
        if(director != null)
        {
            director.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    void SkipCutScenes()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        spawnCutScenes.canSpawn = true;
        spawnCutScenes.SpawnCutScene();
    }
}
