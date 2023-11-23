using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;


public class SpawnCutScenes : MonoBehaviour
{
    public static event Action EndCutScene;
    [SerializeField] public GameObject[] cutScenes;
    [SerializeField] GameObject fadeNextMap;
    [SerializeField] GameObject startGame;
    [HideInInspector] public int indexCutScene = 0;
    public bool canSpawn;
    public bool nextmap;
    public bool destroyCutScenes;
    [HideInInspector] public ControlCutScenes controlCutScenes;
    private void Start()
    {
        SpawnCutScene();
    }
    void OnEnable()
    {
        SpawnCutScene();
    }
    void OnDisable()
    {
        indexCutScene = 0;
        canSpawn = true;
    }
    public void SpawnCutScene()
    {
        //End Cut Scenes
        if (indexCutScene > cutScenes.Length - 1)
        {
            if (destroyCutScenes)
            {
                SpawnCutScenes.EndCutSceneEvent();
            }
            this.gameObject.SetActive(false);
            if (nextmap)
            {
                if (fadeNextMap != null)
                {
                    fadeNextMap.SetActive(true);
                }
                Gamemanager.Instance.NextScenes();
            }
            if (startGame != null)
            {
                startGame.SetActive(true);
            }
            Gamemanager.ChangePlayerMode();
        }
        else
        {
            controlCutScenes = cutScenes[indexCutScene].GetComponent<ControlCutScenes>();
            controlCutScenes.spawnCutScenes = this;
            if (controlCutScenes.CameraScenes)//Check Cut ScenesCamera
            {
                canSpawn = false;
                cutScenes[indexCutScene].SetActive(true);
            }
            if (canSpawn)//Spawn Cut Scenes
            {
                canSpawn = false;
                GameObject c = Instantiate(cutScenes[indexCutScene], transform.position, transform.rotation, transform.parent);
                controlCutScenes = c.GetComponent<ControlCutScenes>();
                controlCutScenes.spawnCutScenes = this;
            }
        }
    }

    public static void EndCutSceneEvent()
    {
        EndCutScene?.Invoke();
    }
}
