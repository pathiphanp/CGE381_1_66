using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class ControlCutScenes : MonoBehaviour
{
    public SpawnCutScenes spawnCutScenes;
    [SerializeField] PlayableDirector director;
    [SerializeField] bool CameraScenes;
    [SerializeField] bool canNotSkipCutScenes;

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
        if (CameraScenes)
        {
            CameraCutScenes();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void CameraCutScenes()
    {
        director.Play();
        Invoke("SkipCutScenes", 4f);
    }
    void SkipCutScenes()
    {
        if (!canNotSkipCutScenes)
        {
            Destroy(this.gameObject);
        }
    }
    void EndCutScenes()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        spawnCutScenes.indexCutScene++;
        spawnCutScenes.canSpawn = true;
        spawnCutScenes.SpawnCutScene();
    }
}
