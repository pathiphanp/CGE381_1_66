using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class ControlCutScenes : MonoBehaviour
{
    public SpawnCutScenes spawnCutScenes;
    [SerializeField] PlayableDirector director;
    [SerializeField] public bool CameraScenes;
    [SerializeField] bool canNotSkipCutScenes;
    [SerializeField] bool startGame;


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
        if (startGame)
        {
            Gamemanager.Instance.cutScenesStartGame = true;
        }
        if (CameraScenes)
        {
            CameraCutScenes();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Gamemanager.ChangeUIMode();
    }
    private void CameraCutScenes()
    {
        director.Play();
        Invoke("EndCutScenes", 4f);
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
        Gamemanager.Instance.cutScenesStartGame = false;
        spawnCutScenes.indexCutScene++;
        spawnCutScenes.canSpawn = true;
        spawnCutScenes.SpawnCutScene();
    }
}
