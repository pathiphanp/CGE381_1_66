using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCutScenes : MonoBehaviour
{
    public static event Action CutSceneStart;
    public SpawnCutScenes spawnCutScenes;

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
        ControlCutScenes.CutSceneStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void StartCutScenesEvent()
    {
        CutSceneStart?.Invoke();
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
