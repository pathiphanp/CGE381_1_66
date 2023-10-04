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
    [SerializeField] public int indexCutScene = 0;
    public bool canSpawn;
    public ControlCutScenes controlCutScenes;
    private void Start()
    {
        SpawnCutScene();
    }

    public void SpawnCutScene()
    {
        if (indexCutScene > cutScenes.Length - 1)
        {
            Gamemanager.ChangePlayerMode();
        }
        else if (cutScenes[indexCutScene].gameObject.scene.name != null)
        {
            controlCutScenes = cutScenes[indexCutScene].GetComponent<ControlCutScenes>();
            controlCutScenes.spawnCutScenes = this;
            canSpawn = false;
            cutScenes[indexCutScene].SetActive(true);
        }
        else
        {
            if (canSpawn)
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
