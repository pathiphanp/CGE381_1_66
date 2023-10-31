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
    [SerializeField] GameObject BgNext;
    [SerializeField] public int indexCutScene = 0;
    public bool canSpawn;
    public bool nextmap;
    public ControlCutScenes controlCutScenes;
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
            this.gameObject.SetActive(false);
            if (nextmap)
            {
                BgNext.SetActive(true);
                Gamemanager.Instance.NextScenes();
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
